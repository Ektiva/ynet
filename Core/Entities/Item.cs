using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int Discount { get; set; }
        public long RatingsCount { get; set; }
        public long RatingsValue { get; set; }
        public long availibilityCount { get; set; }
        public long cartCount { get; set; }
        public List<Color> Color { get; set; }
        public List<Size> Size { get; set; }
        public long Weight { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
