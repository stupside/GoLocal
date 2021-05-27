using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop.Models;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop
{
    public class CreateShopCommand : AbstractRequest<int>
    {
        public string Name { get; init; }
        public LocationDto Location { get; init; }
        public ContactDto Contact { get; init; }
    }
}