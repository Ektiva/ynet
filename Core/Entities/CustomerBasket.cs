using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class CustomerBasket : BaseEntity1
    {
        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        //public string Id { get; set; }
        public List<BasketProduct> Items { get; set; } = new List<BasketProduct>();
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}