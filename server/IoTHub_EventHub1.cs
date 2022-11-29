using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using server.Models;

namespace MCT.Function
{
    public class IoTHub_EventHub1
    {

        
        [FunctionName("IoTHub_EventHub1")]
        public void Run([IoTHubTrigger("messages/events", Connection = "IoThubMessage")]EventData message, ILogger log)
        {
            log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");
            var json = Encoding.UTF8.GetString(message.Body.Array);
            var soldItemMessage = JsonConvert.DeserializeObject<SoldItemMessage>(json);
        }
    }
}