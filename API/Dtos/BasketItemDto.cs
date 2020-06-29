using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Brand { get; set; }

        // public string Description { get; set; }
        public long RatingsCount { get; set; }
        public long RatingsValue { get; set; }
        [Required]
        public long availibilityCount { get; set; }
        public long cartCount { get; set; }
        //public List<string> Color { get; set; }
        //public List<string> Size { get; set; }
        [Required]
        public long Weight { get; set; }
        [Required]
        public string Category { get; set; }
        //public virtual ICollection<List<string>> Images { get; set; }
    }
}