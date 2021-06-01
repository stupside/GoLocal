using System;
using System.Threading.Tasks;
using FluentAssertions;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Enums;
using GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop;
using GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop.Models;
using GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShop;
using GoLocal.Core.Artisan.IntegrationTests.Commons;
using MediatR;
using NUnit.Framework;

namespace GoLocal.Core.Artisan.IntegrationTests.Commands.Shop
{
    public class UpdateShopTests : Fixture
    {
        public UpdateShopTests()
        {
        }
        
        [Test]
        public async Task UpdateShop_ShouldSucceed_WithOldNameEqualNewName_Test()
        {
            var shop = new CreateShopCommand
            {
                Name = Guid.NewGuid().ToString(),
                Location = new LocationDto
                {
                    Street = "Place Doyen Gosse",
                    PostCode = "38000",
                    Country = "France",
                    City = "Grenoble",
                    Address = "2",
                    Region = "Rhone Alpes"
                },
                Contact = new ContactDto
                {
                    Phone = "test",
                    Email = "test@test.com"
                }
            };
            var createShopResult = await Mediator.Send(shop);
            createShopResult.Status.Should().Be(ResultStatus.Ok);
            
            var command = new UpdateShopCommand
            {
                NewName = shop.Name,
                OldName = shop.Name,
                ShopId = createShopResult.TypedEntity,
            };
            
            var result = await Mediator.Send(command);
            result.Status.Should().Be(ResultStatus.Ok);
        }
        
        [Test]
        public async Task UpdateShop_ShouldFail_WithShopAlreadyExist_Test()
        {
            var shop = new CreateShopCommand
            {
                Name = Guid.NewGuid().ToString(),
                Location = new LocationDto
                {
                    Street = "Place Doyen Gosse",
                    PostCode = "38000",
                    Country = "France",
                    City = "Grenoble",
                    Address = "2",
                    Region = "Rhone Alpes"
                },
                Contact = new ContactDto
                {
                    Phone = "test",
                    Email = "test@test.com",
                }
            };
            var shop2 = new CreateShopCommand
            {
                Name = Guid.NewGuid().ToString(),
                Location = new LocationDto
                {
                    Street = "Place Doyen Gosse",
                    PostCode = "38000",
                    Country = "France",
                    City = "Grenoble",
                    Address = "2",
                    Region = "Rhone Alpes"
                },
                Contact = new ContactDto
                {
                    Phone = "test",
                    Email = "test@test.com",
                }
            };
            var createShopResult = await Mediator.Send(shop);
            createShopResult.Status.Should().Be(ResultStatus.Ok);

            var createShop = await Mediator.Send(shop2);
            createShop.Status.Should().Be(ResultStatus.Ok);

            var command = new UpdateShopCommand
            {
                NewName = shop2.Name,
                OldName = shop.Name,
                ShopId = createShopResult.TypedEntity,
            };
            
            var result = await Mediator.Send(command);
            result.Status.Should().Be(ResultStatus.BadRequest);
        }
        
        [Test]
        public async Task UpdateShop_ShouldFail_WithShopNull_Test()
        {
            var shop = new CreateShopCommand
            {
                Name = null,
                Location = new LocationDto
                {
                    Street = null,
                    PostCode = null,
                    Country = null,
                    City = null,
                    Address = null,
                    Region = null
                },
                Contact = new ContactDto
                {
                    Phone = null,
                    Email = null
                }
            };
            var createShopResult = await Mediator.Send(shop);
            createShopResult.Status.Should().Be(ResultStatus.BadRequest);

            var id = new Random();
            var command = new UpdateShopCommand
            {
                NewName = Guid.NewGuid().ToString(),
                OldName = Guid.NewGuid().ToString(),
                ShopId = id.Next()
            };
            
            var result = await Mediator.Send(command);
            result.Status.Should().Be(ResultStatus.NotFound);
        }
    }
}