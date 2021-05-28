using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopImage
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateShopImageCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public IFormFile File { get; init; }

        public UpdateShopImageCommand(int shopId, IFormFile file)
        {
            ShopId = shopId;
            File = file;
        }
    }
}