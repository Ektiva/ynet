using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ItemToReturnDto
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
        public string TechnicalDescription { get; set; }
        public string AdditionalInformation { get; set; }
        public List<string> Color { get; set; }
        public List<string> Size { get; set; }
        public long Weight { get; set; }
        public int CategoryId { get; set; }
        public string BrandName { get; set; }
        public List<ImageToReturnDto> Images { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
