using GoLocal.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShops
{
    public class GetShopsQuery : AbstractPagedRequest<Shop, ShopDto>
    {
        public GetShopsQuery()
        {
            this.ConfigureSearch(m =>
            {
                m.MapFor("id", r => r.Id);
                m.MapFor("name", r => r.Name);
            });
            
            this.ConfigureOrder(m =>
            {
                m.MapFor("id", r => r.Id);
                m.MapFor("name", r => r.Name);
            });
        }
    }
}