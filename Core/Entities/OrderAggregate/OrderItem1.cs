using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.OrderAggregate
{
    public class OrderItem1 : BaseEntity
    {
        public OrderItem1()
        {
        }

        public OrderItem1(ProductItemOrdered1 itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }
        //Snapshot of Item Ordered
        public ProductItemOrdered1 ItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}