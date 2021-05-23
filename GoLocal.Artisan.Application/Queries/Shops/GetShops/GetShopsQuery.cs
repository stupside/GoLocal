using GoLocal.Artisan.Application.Queries.Shops.GetShops.Models;
using GoLocal.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShops
{
    public class GetShopsQuery : AbstractPagedRequest<Shop, ShopDto>
    {
        protected override void ConfigurePaging(PageRequestConfiguration<Shop> paging)
        {
            paging.MapFor("id", r => r.Id);
            paging.MapFor("name", r => r.Name);
        }
    }
}