using CoffeeShop.Domain.Abstractions;
using CoffeeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Business
{
    public class CoffeeServices : ICoffeeServices
    {
        public ICoffeeRepository CoffeeRepository { get; }

        public CoffeeServices(ICoffeeRepository coffeeRepository)
        {
            CoffeeRepository = coffeeRepository
                           ?? throw new ArgumentNullException(nameof(coffeeRepository));
        }

        public async Task<IEnumerable<Coffee>> GetCoffees()
        {
            return await CoffeeRepository.GetCoffees();
        }

        public async Task<Coffee> GetCoffeeById(Guid coffeeId)
        {
            if (coffeeId == Guid.Empty)
                throw new ArgumentException("Invalid Coffee Identifier", nameof(coffeeId));

            return await CoffeeRepository.GetCoffeeById(coffeeId);
        }

        public async Task<bool> CreateCoffee(Coffee coffee)
        {
            var existingCoffee = CoffeeRepository.GetCoffeeById(coffee.CoffeeId);

            if (existingCoffee.Result != null)
                return false;

            return await CoffeeRepository.CreateCoffee(coffee);
        }

        public async Task<bool> UpdateCoffee(Coffee coffee)
        {
            var existingCoffee = CoffeeRepository.GetCoffeeById(coffee.CoffeeId);

            if (existingCoffee.Result == null)
                return false;

            return await CoffeeRepository.UpdateCoffee(coffee);
        }

        public async Task<bool> DeleteCoffee(Guid coffeeId)
        {
            if (coffeeId == Guid.Empty)
                throw new ArgumentException("Invalid Coffee Identifier", nameof(coffeeId));

            return await CoffeeRepository.DeleteCoffee(coffeeId);
        }
    }
}