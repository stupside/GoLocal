using System;
using System.Collections.Generic;
using GoLocal.Bus.Authorizer.Commons.Requirement;

namespace GoLocal.Bus.Authorizer.Configurations
{
    public interface IAuthorizerConfiguration
    {
        Type Type { get; }
        public List<IAuthorizerRequirement> Configurations { get; }
        public void AddRequirement(IAuthorizerRequirement requirement);
        IEnumerable<TConfiguration> GetConfigurations<TConfiguration>() where TConfiguration : IAuthorizerRequirement;
    }
}