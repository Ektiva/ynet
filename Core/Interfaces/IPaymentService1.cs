using Core.Entities;
using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaymentService1
    {
        Task<CustomerBasket1> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order1> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<Order1> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}