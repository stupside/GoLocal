using System;
using System.Collections.Generic;
using System.Linq;
using GoLocal.Bus.Authorizer.Commons.Requirement;

namespace GoLocal.Bus.Authorizer.Configurations
{
    public class AbstractAuthorizerConfiguration<TEntity> : IAuthorizerConfiguration
    {
        public Type Type => typeof(TEntity);
        public List<IAuthorizerRequirement> Configurations { get; }

        public AbstractAuthorizerConfiguration()
        {
            Configurations = new List<IAuthorizerRequirement>();
        }

        public void AddRequirement(IAuthorizerRequirement requirement)
            => Configurations.Add(requirement);

        public IEnumerable<TConfiguration> GetConfigurations<TConfiguration>()
            where TConfiguration : IAuthorizerRequirement
            => Configurations.OfType<TConfiguration>();
    }
}