using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemImage
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateItemImageCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public IFormFile File { get; init; }

        public UpdateItemImageCommand(int shopId, int itemId, IFormFile file)
        {
            ShopId = shopId;
            ItemId = itemId;
            File = file;
        }
    }
}