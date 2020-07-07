using API.Dtos;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class OrderItem1UrlResolver : IValueResolver<OrderItem1, OrderItemDto, string>
    {
        private readonly IConfiguration _config;
        public OrderItem1UrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(OrderItem1 source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return _config["ApiUrl"] + source.ItemOrdered.PictureUrl;
            }

            return null;
        }
    }
}