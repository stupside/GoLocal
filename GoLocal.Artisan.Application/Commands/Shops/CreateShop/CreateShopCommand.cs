using GoLocal.Artisan.Application.Commands.Shops.CreateShop.Models;
using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Shops.CreateShop
{
    public class CreateShopCommand : AbstractRequest<int>
    {
        public string Name { get; init; }
        public LocationDto Location { get; init; }
        public ContactDto Contact { get; init; }
    }
}