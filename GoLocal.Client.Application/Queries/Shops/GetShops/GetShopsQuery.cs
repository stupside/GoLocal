using GoLocal.Client.Application.Queries.Shops.GetShops.Models;
using GoLocal.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Client.Application.Queries.Shops.GetShops
{
    public class GetShopsQuery : AbstractPagedRequest<Shop, ShopDto>
    {
        public int Range { get; init; }
        public string Location { get; init; }
        public string Name { get; init; }
        
        protected override void ConfigurePaging(PageRequestConfiguration<Shop> paging)
        {
            paging.MapFor("name", r => r.Name);
        }
    }
}