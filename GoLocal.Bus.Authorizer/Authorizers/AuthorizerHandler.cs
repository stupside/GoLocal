using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Authorizer.Commons.Helpers;
using GoLocal.Bus.Authorizer.Commons.Requirement.Must;
using GoLocal.Bus.Authorizer.Commons.Responses;
using GoLocal.Bus.Authorizer.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Bus.Authorizer.Authorizers
{
    public class AuthorizerHandler<TUser, TContext> : IAuthorizerHandler
        where TUser : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IEnumerable<IAuthorizerConfiguration> _containers;
        private readonly IUserAccessor<TUser> _accessor;
        
        public AuthorizerHandler(IEnumerable<IAuthorizerConfiguration> configurations, IUserAccessor<TUser> accessor, TContext context)
        {
            _accessor = accessor;
            _containers = configurations;
            _context = context;
        }

        public async Task<AuthorizerResult> Handle<TRequest>(TRequest request)
        {
            var authorized = request.GetAuthorized();
            if (authorized == null)
                return new AuthorizerResult();

            var id = request.GetAuthorizedId();
            var entity = await _context.FindAsync(authorized.Type, id);
            if (entity == null)
                return new AuthorizerResult().IsNotFound();

            var container = _containers.GetContainerForType(authorized);
            
            var configurations = container.GetConfigurations<MustRequirement>().ToList();
            if (!configurations.Any())
                throw new Exception($"Authorizer attributes found for {nameof(TRequest)} but no configuration was found");
            
            foreach (var configuration in configurations)
            {
                bool validated = configuration.MustType switch
                {
                    MustType.Owner => configuration.IsValidConstraint(entity, await _accessor.GetUserIdAsync()),
                    MustType.Equal => configuration.IsValidConstraint(entity),
                    _ => false
                };

                if (!validated)
                    return new AuthorizerResult().IsUnauthorized();
            }
            return new AuthorizerResult();
        }
    }
}