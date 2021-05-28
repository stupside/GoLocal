using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Application.Queries.Shops.GetShops.Models;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShops
{
    public class GetShopsQuery : AbstractPagedRequest<Shop, ShopDto>
    {
        public double Range { get; init; }
        public string Location { get; init; }
        public string Name { get; init; }
        
        protected override void ConfigurePaging(PageRequestConfiguration<Shop> paging)
        {
            paging.MapFor("name", r => r.Name);
        }
    }
}