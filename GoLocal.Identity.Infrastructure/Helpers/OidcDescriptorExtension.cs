using System;
using System.Threading;
using System.Threading.Tasks;
using OpenIddict.Abstractions;

namespace GoLocal.Identity.Infrastructure.Helpers
{
    public static class OidcDescriptorExtension
    {
        public static async Task AddClient(this IOpenIddictApplicationManager manager, Action<OpenIddictApplicationDescriptor> descriptor, CancellationToken cancellationToken =  default)
        {
            var application = new OpenIddictApplicationDescriptor();
            descriptor(application);

            if (string.IsNullOrEmpty(application.ClientId))
                throw new ArgumentNullException();
            
            if (await manager.FindByClientIdAsync(application.ClientId, cancellationToken) is not null)
                return;

            await manager.CreateAsync(application, cancellationToken);
        }
    }
}