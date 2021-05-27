using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Authorizer.Commons.Requirement;
using GoLocal.Bus.Authorizer.Configurations;

namespace GoLocal.Bus.Authorizer.Commons.Helpers
{
    public static class AuthorizerExtension
    {
        public static AuthorizedEntityAttribute GetAuthorized<TRequest>(this TRequest request)
            => typeof(TRequest).GetCustomAttribute<AuthorizedEntityAttribute>();

        public static object GetAuthorizedId<TRequest>(this TRequest request)
        {
            if (!request.IsAuthorized())
                throw new InvalidConstraintException($"The request is not decorated with '{nameof(AuthorizedEntityAttribute)}'");
            
            var id = typeof(TRequest).GetProperties()
                .SingleOrDefault(m => m.GetCustomAttribute<AuthorizedEntityIdAttribute>(true) != null);

            if (id == null)
                throw new InvalidConstraintException($"The request is not decorated with '{nameof(AuthorizedEntityIdAttribute)}'");

            return id.GetValue(request);
        }
        
        public static bool IsAuthorized<TRequest>(this TRequest request)
            => request.GetAuthorized() != null;

        public static IAuthorizerConfiguration GetContainerForType(
            this IEnumerable<IAuthorizerConfiguration> containers, AuthorizedEntityAttribute attribute)
        {
            var container = containers.Where(m => m.Type == attribute.Type).ToList();
            if (container == null || !container.Any())
                throw new NotImplementedException();

            if (container.Count() > 1)
                throw new InvalidConstraintException($"Too many configurations for the type '{attribute.Type.Name}'");

            return container.First();
        }

        public static TConfiguration GetFirstConfiguration<TConfiguration>(this IAuthorizerConfiguration container)
            where TConfiguration : IAuthorizerRequirement
            => container.GetConfigurations<TConfiguration>().FirstOrDefault();
    }
}