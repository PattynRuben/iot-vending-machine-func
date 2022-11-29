using System;
using Newtonsoft.Json;

namespace server.Models
{
    public class DrankPrices
    {
        [JsonProperty("water")]
        public int Water { get; set; }
        [JsonProperty("cola")]
        public int Cola { get; set; }
        [JsonProperty("fruitsap")]
        public int Fruitsap { get; set; }
    }
}
