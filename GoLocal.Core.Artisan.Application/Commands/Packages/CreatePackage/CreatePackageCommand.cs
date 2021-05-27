using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Packages.CreatePackage
{
    [AuthorizedEntity(typeof(Shop))]
    public class CreatePackageCommand : AbstractRequest<int>
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public float Price { get; init; }
        public int Stocks { get; init; }
    }
}