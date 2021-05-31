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
        public async Task CreateShopWithoutLocation_Test()
        {
            var result = await Mediator.Send(new CreateShopCommand
            {
                Name = "Test",
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
                    Phone = "test",
                    Email = "test@test.com"
                }
            });
            result.Status.Should().Be(ResultStatus.BadRequest);
        }

        [Test]
        public async Task CreateShopWithEmailInvalid_Test()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public async Task CreateShopWithEmailValid_Test()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public async Task CreateShopWithLocationInvalid_Test()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public async Task CreateShopWithLocationValid_Test()
        {
            throw new NotImplementedException();
        }
    }
}