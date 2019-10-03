using CoffeeShop.Domain.Abstractions;
using CoffeeShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CoffeeShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        public ICoffeeServices CoffeeServices { get; set; }

        public CoffeeController(ICoffeeServices coffeeServices)
        {
            CoffeeServices = coffeeServices ?? throw new ArgumentNullException(nameof(coffeeServices));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await CoffeeServices.GetCoffees());

        [HttpGet("{coffeeId}")]
        public async Task<IActionResult> GetById(Guid coffeeId)
        {
            var existingCoffee = await CoffeeServices.GetCoffeeById(coffeeId);
            return existingCoffee != null ? (IActionResult)Ok(existingCoffee) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Coffee coffee)
            => await CoffeeServices.CreateCoffee(coffee)
                ? Created(string.Empty, coffee) as IActionResult
                    : StatusCode((int)HttpStatusCode.NotModified);

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Coffee coffee)
            => await CoffeeServices.UpdateCoffee(coffee)
                ? Ok() as IActionResult
                    : StatusCode((int)HttpStatusCode.NotModified);

        [HttpDelete("{coffeeId}")]
        public async Task<IActionResult> Delete(Guid coffeeId)
            => await CoffeeServices.DeleteCoffee(coffeeId)
                ? Ok() as IActionResult
                    : StatusCode((int)HttpStatusCode.NotModified);
    }
}