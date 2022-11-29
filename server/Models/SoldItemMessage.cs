using System;

namespace server.Models
{
    public class SoldItemMessage
    {
        public string Id {get;set;}
        public string Product { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string Location { get; set; }
    }
}
