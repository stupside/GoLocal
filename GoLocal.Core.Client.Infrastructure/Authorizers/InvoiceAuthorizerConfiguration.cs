using GoLocal.Bus.Authorizer.Commons.Requirement.Must;
using GoLocal.Bus.Authorizer.Configurations;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Client.Infrastructure.Authorizers
{
    public class InvoiceAuthorizerConfiguration : AbstractAuthorizerConfiguration<Invoice>
    {
        public InvoiceAuthorizerConfiguration()
        {
            this.With(m => m.UserId).Owner();
        }
    }
}