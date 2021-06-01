using System;
using System.Threading.Tasks;
using GoLocal.Bus.Results.Enums;
using GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop;
using GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop.Models;
using GoLocal.Core.Artisan.IntegrationTests.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace GoLocal.Core.Artisan.IntegrationTests.Commands.Shop
{
    public class CreateShopTests : Fixture
    {
        public CreateShopTests()
        {
        }

        [Test]
        public async Task CreateShop_ShouldFail_WithInvalidLocation_Test()
        {
            var command = new CreateShopCommand
            {
                Name = Guid.NewGuid().ToString(),
                
                Location = new LocationDto
                {
                    Street = "FakeStreet",
                    PostCode = "FakeCode",
                    Country = "FakeCountry",
                    City = "FakeCity",
                    Address = "FakeAddress",
                    Region = "FakeRegion"
                },

                Contact = new ContactDto
                {
                    Phone = "test",
                    Email = "test@test.com"
                }
            };

            var result = await Mediator.Send(command);
            result.Status.Should().Be(ResultStatus.BadRequest);
        }
        
        [Test]
        public async Task CreateShop_ShouldSucceed_WithValidLocation_Test()
        {
            var command = new CreateShopCommand
            {
                Name = Guid.NewGuid().ToString(),
                
                Location = new LocationDto
                {
                    Street = "Place Doyen Gosse",
                    PostCode = "38000",
                    Country = "France",
                    City = "Grenoble",
                    Address = "2",
                    Region = "Isère"
                },

                Contact = new ContactDto
                {
                    Phone = "test",
                    Email = "test@test.com",
                }
            };
            
            var result = await Mediator.Send(command);
            result.Status.Should().Be(ResultStatus.Ok);
        }
        
     
        [Test]
        public async Task CreateShop_ShouldFail_WithNameAlreadyExist_Test()
        {
            var command = new CreateShopCommand 
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

            var result = await Mediator.Send(command);
            result.Status.Should().Be(ResultStatus.Ok);
            
            var failed = await Mediator.Send(command);
            failed.Status.Should().Be(ResultStatus.BadRequest);
        }
        
        [Test]
        public async Task CreateShop_ShouldSucceed_WithUniqueName_Test()
        {
            var command = new CreateShopCommand
            {
                Name = "UniqueName",
                
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

            var result = await Mediator.Send(command);
            result.Status.Should().Be(ResultStatus.Ok);
        }
    }
}