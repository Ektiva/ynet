using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderService1
    {
        Task<Order1> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
        Task<IReadOnlyList<Order1>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order1> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}