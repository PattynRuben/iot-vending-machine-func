using System;
using Newtonsoft.Json;
namespace automaat.Models
{
    public class DesiredProperties
    {
        [JsonProperty("priceWater")]
        public int PriceWater { get; set; }
        [JsonProperty("priceCola")]
        public int PriceCola { get; set; }
        [JsonProperty("priceFruitsap")]
        public int PriceFruitsap { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
