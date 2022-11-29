using System.Text;
using automaat.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;

DesiredProperties desiredProperties = new DesiredProperties();

string connectionString = "HostName=mctiothub.azure-devices.net;DeviceId=drankautomaat;SharedAccessKey=k2QJOZ729hqyZEmRZvGA4LWIBUZVTgi1AeEXwKvI7lM=";
var deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
await deviceClient.SetDesiredPropertyUpdateCallbackAsync(OnDesiredPropertiesUpdate, null);
#region Boot

async Task GetDesiredProperties()
{
    var twin = await deviceClient.GetTwinAsync();
    var desiredPropertiesJson = twin.Properties.Desired.ToJson();
    Console.WriteLine(desiredPropertiesJson);
    desiredProperties = JsonConvert.DeserializeObject<DesiredProperties>(desiredPropertiesJson);
}

#endregion

#region Menu

async Task OnDesiredPropertiesUpdate(TwinCollection dp, object userContext)
{
    var desiredPropertiesJson = dp.ToJson();
    Console.WriteLine(desiredPropertiesJson);
    desiredProperties = JsonConvert.DeserializeObject<DesiredProperties>(desiredPropertiesJson);
}
await GetDesiredProperties();
await Loop();
async Task Loop()
{
    while (true)
    {
        Console.WriteLine("Maak uw keuze: ");
        Console.WriteLine("1. Water");
        Console.WriteLine("2. Cola");
        Console.WriteLine("3. Fruitsap");
        Console.WriteLine("4. Afsluiten");

        string keuze = Console.ReadLine();
        await ProcessChoice(keuze);
    }
}
async Task ProcessChoice(string choise)
{
    Console.WriteLine("Maak uw keuze: ");
    int aantal = Convert.ToInt32(Console.ReadLine());
    switch (choise)
    {

        case "1":
            await SendWater(aantal);
            break;
        case "2":
            await SendCola(aantal);
            break;
        case "3":
            await SendFruitsap(aantal);
            break;
        case "4":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Ongeldige keuze");
            break;
    }
}

async Task SendWater(int aantal)
{
    SoldItemMessage message = new SoldItemMessage()
    {
        Product = "Water",
        Amount = aantal,
        Price = desiredProperties.PriceWater,
        Total = desiredProperties.PriceWater*aantal,
        Location = desiredProperties.Location
    };
    string json = JsonConvert.SerializeObject(message);
    var payload = new Message(Encoding.UTF8.GetBytes(json));
    await deviceClient.SendEventAsync(payload);
}

async Task SendCola(int aantal)
{
    SoldItemMessage message = new SoldItemMessage()
    {
        Product = "Cola",
        Amount = aantal,
        Price = desiredProperties.PriceCola,
        Total = desiredProperties.PriceCola * aantal,
        Location = desiredProperties.Location
    };
    string json = JsonConvert.SerializeObject(message);
    var payload = new Message(Encoding.UTF8.GetBytes(json));
    await deviceClient.SendEventAsync(payload);
}
async Task SendFruitsap(int aantal)
{
    SoldItemMessage message = new SoldItemMessage()
    {
        Product = "Fruitsap",
        Amount = aantal,
        Price = desiredProperties.PriceFruitsap,
        Total = desiredProperties.PriceFruitsap * aantal,
        Location = desiredProperties.Location
    };
    string json = JsonConvert.SerializeObject(message);
    var payload = new Message(Encoding.UTF8.GetBytes(json));
    await deviceClient.SendEventAsync(payload);
}
#endregion