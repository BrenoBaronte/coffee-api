using CoffeeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Abstractions
{
    public interface ICoffeeRepository
    {
        Task<IEnumerable<Coffee>> GetCoffees();

        Task<Coffee> GetCoffeeById(Guid id);

        Task<bool> CreateCoffee(Coffee coffee);

        Task<bool> UpdateCoffee(Coffee coffee);

        Task<bool> DeleteCoffee(Guid coffeeId);
    }
}