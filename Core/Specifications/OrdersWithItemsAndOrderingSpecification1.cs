using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrderingSpecification1 : BaseSpecification<Order1>
    {
        public OrdersWithItemsAndOrderingSpecification1(string email) : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrdersWithItemsAndOrderingSpecification1(int id, string email)
            : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
