using System.Threading.Tasks;
using GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop;
using GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop.Models;
using GoLocal.Core.Artisan.IntegrationTests.Commons;
using NUnit.Framework;

namespace GoLocal.Core.Artisan.IntegrationTests.Commands.Shop
{
    public class CreateShopTests : Fixture
    {
        public CreateShopTests()
        {
        }
        
        [Test]
        public async Task CreateShopTest()
        {
            var result = await Mediator.Send(new CreateShopCommand
            {
                Name = "Test",
                Location = new LocationDto
                {
                    Street = "Doyen Gosse",
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
            });
        }
    }
}