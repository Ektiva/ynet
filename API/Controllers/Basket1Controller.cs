using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class Basket1Controller : BaseApiController
    {
        private readonly IBasketRepository<CustomerBasket1> _basketRepository;
        private readonly IMapper _mapper;
        public Basket1Controller(IBasketRepository<CustomerBasket1> basketRepository, IMapper mapper)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket1>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket1(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket1>> UpdateBasket(CustomerBasket1 basket)
        {
            //var customerBasket = _mapper.Map<CustomerBasket1Dto, CustomerBasket1>(basket);

            var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}