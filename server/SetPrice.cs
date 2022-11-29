using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using server.Models;

namespace MCT.Function
{
    public static class SetPrice
    {
        [FunctionName("SetPrice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = "prices/{deviceId}")] HttpRequest req,
            string deviceId,ILogger log)
        {
            try
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var drankPrices = JsonConvert.DeserializeObject<DrankPrices>(body);

                var connectionString = Environment.GetEnvironmentVariable("IoTHubAdmin");
                var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
                var twin = await registryManager.GetTwinAsync(deviceId);
                twin.Properties.Desired["priceWater"] = drankPrices.Water;
                twin.Properties.Desired["priceCola"] = drankPrices.Cola;
                twin.Properties.Desired["priceFruitsap"] = drankPrices.Fruitsap;
                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new StatusCodeResult(500);
            }

        }
    }
}
