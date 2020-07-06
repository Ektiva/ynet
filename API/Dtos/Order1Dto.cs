using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class Order1Dto
    {
        public string CartId { get; set; }
        public int DeliveryMethodId { get; set; }
        public Address1Dto ShipToAddress { get; set; }
    }
}
