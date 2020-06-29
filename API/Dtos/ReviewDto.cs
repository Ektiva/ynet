using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ReviewDto
    {
        [Required]
        public string ReviewerName { get; set; }
        //[Required]
        //[EmailAddress]
        //public string ReviewerEmail { get; set; }
        public string ReviewerPhoto { get; set; }
        [Required]
        public string ReviewMessage { get; set; }
        public DateTimeOffset ReviewDate { get; set; } = DateTimeOffset.Now;
        [Required]
        [Range(1, 5,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int rate { get; set; }
        public string sentiment { get; set; }
    }
}
