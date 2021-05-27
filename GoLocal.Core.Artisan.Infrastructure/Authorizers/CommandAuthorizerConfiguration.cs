using GoLocal.Bus.Authorizer.Commons.Requirement.Must;
using GoLocal.Bus.Authorizer.Configurations;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Infrastructure.Authorizers
{
    public class CommandAuthorizerConfiguration : AbstractAuthorizerConfiguration<Command>
    {
        public CommandAuthorizerConfiguration()
        {
            this.With(m => m.Package.Item.Shop.UserId).Owner();
        }
    }
}