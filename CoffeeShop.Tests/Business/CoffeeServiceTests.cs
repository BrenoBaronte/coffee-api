using AutoFixture.Idioms;
using CoffeeShop.Business;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Tests.AutoData;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace CoffeeShop.Tests.Business
{
    public class CoffeeServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void GuardClausesConstructorTest(GuardClauseAssertion guardClause)
        {
            guardClause.Verify(typeof(CoffeeServices).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Get_ShouldCallMethodsCorrectly(CoffeeServices sut)
        {
            await sut.GetCoffees();
            await sut.CoffeeRepository.Received(1).GetCoffees();
        }

        [Theory, AutoNSubstituteData]
        public async void GetById_ShouldCallMethodsCorrectly(Guid coffeeId, CoffeeServices sut)
        {
            await sut.GetCoffeeById(coffeeId);
            await sut.CoffeeRepository.Received(1).GetCoffeeById(Arg.Is(coffeeId));
        }

        [Theory, AutoNSubstituteData]
        public async void GetById_WhenCoffeeIdEmpty_ShouldThrowArgumentException(CoffeeServices sut)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetCoffeeById(Guid.Empty));
            await sut.CoffeeRepository.DidNotReceive().GetCoffeeById(Arg.Any<Guid>());
        }

        [Theory, AutoNSubstituteData]
        public async void CreateCoffee_ShouldCallMethodsCorrectly(Coffee coffee, CoffeeServices sut)
        {
            await sut.CreateCoffee(coffee);
            await sut.CoffeeRepository.Received(1).CreateCoffee(Arg.Is(coffee));
        }

        [Theory, AutoNSubstituteData]
        public async void CreateCoffee_WhenCoffeeIdAlreadyExists_ShouldFails(Coffee coffee, CoffeeServices sut)
        {
            sut.CoffeeRepository.GetCoffeeById(Arg.Is(coffee.CoffeeId)).Returns(coffee);
            var result = await sut.CreateCoffee(coffee);
            result.Should().BeFalse();
            await sut.CoffeeRepository.DidNotReceive().CreateCoffee(Arg.Any<Coffee>());
        }

        [Theory, AutoNSubstituteData]
        public async void UpdateCoffee_ShouldCallMethodsCorrectly(Coffee coffee, CoffeeServices sut)
        {
            sut.CoffeeRepository.GetCoffeeById(Arg.Is(coffee.CoffeeId)).Returns(coffee);    
            await sut.UpdateCoffee(coffee);
            await sut.CoffeeRepository.Received(1).UpdateCoffee(Arg.Is(coffee));
        }

        [Theory, AutoNSubstituteData]
        public async void UpdateCoffee_WhenCoffeeNotExists_ShouldFails(Coffee coffee, CoffeeServices sut)
        {
            await sut.UpdateCoffee(coffee);
            await sut.CoffeeRepository.DidNotReceive().UpdateCoffee(Arg.Any<Coffee>());
        }

        [Theory, AutoNSubstituteData]
        public async void DeleteCoffee_ShouldCallMethodsCorrectly(Guid coffeeId, CoffeeServices sut)
        {
            await sut.DeleteCoffee(coffeeId);
            await sut.CoffeeRepository.Received(1).DeleteCoffee(Arg.Is(coffeeId));
        }

        [Theory, AutoNSubstituteData]
        public async void DeleteCoffee_WhenCoffeeIdIsEmpty_ShouldThrowArgumentException(CoffeeServices sut)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => sut.DeleteCoffee(Guid.Empty));
            await sut.CoffeeRepository.DidNotReceive().DeleteCoffee(Arg.Any<Guid>());
        }

    }
}
