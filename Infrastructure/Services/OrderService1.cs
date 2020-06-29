using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService1 : IOrderService1
    {
        private readonly IBasketRepository<CustomerBasket1> _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;
        //private readonly IPaymentService _paymentService;
        public OrderService1(IBasketRepository<CustomerBasket1> basketRepo, IUnitOfWork unitOfWork/*, IPaymentService paymentService*/)
        {
            //_paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }

        public async Task<Order1> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // get items from the product repo
            var items = new List<OrderItem1>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Item>().GetByIdAsync(item.Id);

                // var picture = await _context.Images.Where(x => x.ItemId == item.Id).FirstOrDefaultAsync();

                var itemOrdered = new ProductItemOrdered1(productItem.Id, productItem.Name, "url here");
                var orderItem = new OrderItem1(itemOrdered, productItem.NewPrice, item.Quantity);
                items.Add(orderItem);
            }

            // get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // calculate subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //// check to see if order exists
            //var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
            //var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            //if (existingOrder != null)
            //{
            //    _unitOfWork.Repository<Order>().Delete(existingOrder);
            //    await _paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
            //}

            // create order
            var order = new Order1(items, buyerEmail, shippingAddress, deliveryMethod, subtotal/*, basket.PaymentIntentId*/);
            _unitOfWork.Repository<Order1>().Add(order);

            // save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // delete basket
            await _basketRepo.DeleteBasketAsync(basketId);

            // return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order1> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification1(id, buyerEmail);

            return await _unitOfWork.Repository<Order1>().GetEntityWithSpec(spec);

        }

        public async Task<IReadOnlyList<Order1>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification1(buyerEmail);

            return await _unitOfWork.Repository<Order1>().ListAsync(spec);
        }
    }
}