using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBasketRepository<T> where T : BaseEntity1
    {
        Task<T> GetBasketAsync(string basketId);
        Task<T> UpdateBasketAsync(T basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
