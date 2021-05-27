using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Artisan.Application.Queries.Shops.GetShops.Models;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Queries.Shops.GetShops
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