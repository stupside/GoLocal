using GoLocal.Bus.Authorizer.Commons.Requirement.Must;
using GoLocal.Bus.Authorizer.Configurations;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Artisan.Infrastructure.Authorizers
{
    public class ShopAuthorizerConfiguration : AbstractAuthorizerConfiguration<Shop>
    {
        public ShopAuthorizerConfiguration()
        {
            this.With(m => m.UserId).Owner();
        }
    }
}