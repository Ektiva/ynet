using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ItemForServBus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int Discount { get; set; }
        public long RatingsCount { get; set; }
        public long RatingsValue { get; set; }
        public long availibilityCount { get; set; }
        public long cartCount { get; set; }
        public long Weight { get; set; }
        public int CategoryId { get; set; }
        public string BrandName { get; set; }
    }
}
