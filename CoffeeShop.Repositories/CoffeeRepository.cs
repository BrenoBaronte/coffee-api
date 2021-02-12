using CoffeeShop.Domain.Abstractions;
using CoffeeShop.Domain.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private const string InsertCommandText =
            "INSERT INTO [dbo].[Coffee] (CoffeeId, Name, Description) " +
            "VALUES (@CoffeeId, @Name, @Description)";

        private const string UpdateCommandText =
            "UPDATE [dbo].[Coffee] " +
            "SET [Name] = @Name, [Description] = @Description " +
            "WHERE [CoffeeId] = @CoffeeId";

        private const string DeleteCommandText =
            "DELETE FROM [dbo].[Coffee] WHERE [CoffeeId] = @CoffeeId";

        public async Task<bool> CreateCoffee(Coffee coffee)
        {
            using (var connnection = new SqlConnection("_CS_"))
            {
                connnection.Open();

                var rollsAffected = await connnection.ExecuteAsync(InsertCommandText, coffee);

                return rollsAffected == 1;
            }
        }

        public async Task<bool> DeleteCoffee(Guid coffeeId)
        {
            using (var connnection = new SqlConnection("_CS_"))
            {
                connnection.Open();

                var rollsAffected = await connnection.ExecuteAsync(DeleteCommandText, new { coffeeId });

                return rollsAffected == 1;
            }
        }

        public async Task<Coffee> GetCoffeeById(Guid coffeeId)
        {
            using (var connnection = new SqlConnection("_CS_"))
            {
                connnection.Open();

                var coffee = await connnection.QueryAsync<Coffee>
                    ("SELECT * FROM [dbo].[Coffee] (NOLOCK) WHERE [CoffeeId] = @coffeeId",
                    new { coffeeId });

                return coffee.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Coffee>> GetCoffees()
        {
            using (var connnection = new SqlConnection("_CS_"))
            {
                connnection.Open();

                var coffees = await connnection.QueryAsync<Coffee>
                    ("SELECT * FROM [dbo].[Coffee] (NOLOCK)");

                return coffees;
            }
        }

        public async Task<bool> UpdateCoffee(Coffee coffee)
        {
            using (var connnection = new SqlConnection("_CS_"))
            {
                connnection.Open();

                var rollsAffecteds = await connnection.ExecuteAsync(UpdateCommandText, coffee);

                return rollsAffecteds == 1;
            }
        }
    }
}
