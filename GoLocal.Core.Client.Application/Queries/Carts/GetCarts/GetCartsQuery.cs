using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Application.Queries.Carts.GetCarts.Models;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Client.Application.Queries.Carts.GetCarts
{
    public class GetCartsQuery : AbstractPagedRequest<Cart, CartDto>
    {
        public int ShopId { get; init; }
        protected override void ConfigurePaging(PageRequestConfiguration<Cart> paging)
        {
            paging.MapFor("sid", m => m.ShopId);
        }
    }
}