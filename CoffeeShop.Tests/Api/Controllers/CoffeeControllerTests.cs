using AutoFixture.Idioms;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Tests.AutoData;
using CoffeeShopApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace CoffeeShop.Tests.Api.Controllers
{
    public class CoffeeControllerTests
    {
        [Theory, AutoNSubstituteData]
        public void GuardTests(GuardClauseAssertion guardClause)
        {
            guardClause.Verify(typeof(CoffeeController).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Get_ShouldCallsMethodsCorrectly(List<Coffee> coffees, CoffeeController sut)
        {
            sut.CoffeeServices.GetCoffees().Returns(coffees);

            var result = await sut.Get();

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            await sut.CoffeeServices.Received(1).GetCoffees();
        }

        [Theory, AutoNSubstituteData]
        public async void GetbyId_ShouldCallMethodsCorrectly(Guid coffeeId, Coffee coffee, CoffeeController sut)
        {
            sut.CoffeeServices.GetCoffeeById(Arg.Is(coffeeId)).Returns(coffee);

            var result = await sut.GetById(coffeeId);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            await sut.CoffeeServices.Received(1).GetCoffeeById(Arg.Is(coffeeId));
        }

        [Theory, AutoNSubstituteData]
        public async void GetbyId_WhenCoffeeIdNotExists_ShouldReturnNotFound(Guid coffeeId, CoffeeController sut)
        {
            var result = await sut.GetById(coffeeId);

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
            ((NotFoundResult)result).StatusCode.Should().Be(404);
            await sut.CoffeeServices.Received(1).GetCoffeeById(Arg.Is(coffeeId));
        }

        [Theory, AutoNSubstituteData]
        public async void Post_ShouldCallMethodsCorrectly(Coffee coffee, CoffeeController sut)
        {
            sut.CoffeeServices.CreateCoffee(Arg.Any<Coffee>()).Returns(true);

            var result = await sut.Post(coffee);

            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();

            ((CreatedResult)result).StatusCode.Should().Be(201);
            await sut.CoffeeServices.Received(1).CreateCoffee(Arg.Is(coffee));
        }

        [Theory, AutoNSubstituteData]
        public async void Post_WhenSaveFails_ShouldCallMethodsCorrectly(Coffee coffee, CoffeeController sut)
        {
            sut.CoffeeServices.CreateCoffee(Arg.Any<Coffee>()).Returns(false);

            var result = await sut.Post(coffee);

            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>();

            ((StatusCodeResult)result).StatusCode.Should().Be(304);
        }

        [Theory, AutoNSubstituteData]
        public async void Put_ShouldCallMethodsCorrectly(Coffee coffee, CoffeeController sut)
        {
            sut.CoffeeServices.UpdateCoffee(Arg.Is(coffee)).Returns(true);

            var result = await sut.Put(coffee);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();

            ((OkResult)result).StatusCode.Should().Be(200);
            await sut.CoffeeServices.Received(1).UpdateCoffee(Arg.Is(coffee));
        }

        [Theory, AutoNSubstituteData]
        public async void Put_WhenUpdateFails_ShouldCallMethodsCorrectly(Coffee coffee, CoffeeController sut)
        {
            sut.CoffeeServices.UpdateCoffee(Arg.Any<Coffee>()).Returns(false);

            var result = await sut.Put(coffee);

            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>();

            ((StatusCodeResult)result).StatusCode.Should().Be(304);
        }

        [Theory, AutoNSubstituteData]
        public async void Delete_ShouldCallMethodsCorrectly(Guid coffeeId, CoffeeController sut)
        {
            sut.CoffeeServices.DeleteCoffee(Arg.Is(coffeeId)).Returns(true);

            var result = await sut.Delete(coffeeId);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();

            ((StatusCodeResult)result).StatusCode.Should().Be(200);
            await sut.CoffeeServices.Received(1).DeleteCoffee(Arg.Is(coffeeId));
        }

        [Theory, AutoNSubstituteData]
        public async void Delete_WhenOperationFails_ShouldCallMethodsCorrectly(Guid coffeeId, CoffeeController sut)
        {
            sut.CoffeeServices.DeleteCoffee(Arg.Is(coffeeId)).Returns(false);

            var result = await sut.Delete(coffeeId);

            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>();

            ((StatusCodeResult)result).StatusCode.Should().Be(304);
            await sut.CoffeeServices.Received(1).DeleteCoffee(Arg.Is(coffeeId));
        }
    }
}