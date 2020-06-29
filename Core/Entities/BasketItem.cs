using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        // public string Description { get; set; }
        public long RatingsCount { get; set; }
        public long RatingsValue { get; set; }
        public long AvailibilityCount { get; set; }
        public long CartCount { get; set; }
        //public List<string> Color { get; set; }
        //public List<string> Size { get; set; }
        public long Weight { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        //public virtual List<List<string>> Images { get; set; }
    }
}