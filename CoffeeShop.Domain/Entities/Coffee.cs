using System;

namespace CoffeeShop.Domain.Entities
{
    public class Coffee
    {
        public Guid CoffeeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}