using System;
using System.Linq.Expressions;
using GoLocal.Bus.Authorizer.Configurations;

namespace GoLocal.Bus.Authorizer.Commons.Requirement.Must
{
    public static class MustRequirementBuilder
    {
        public static MustRequirement With<TEntity, T>(this AbstractAuthorizerConfiguration<TEntity> configuration, Expression<Func<TEntity, T>> identifier)
        {
            var requirement = new MustRequirement().WithIdentifier(identifier);
            configuration.AddRequirement(requirement);
            return requirement;
        }

        public static void EqualTo(this MustRequirement requirement, object constraint)
            => requirement.WithMustType(MustType.Equal).WithConstraint(constraint);
        
        public static void NotEqualTo(this MustRequirement requirement, object constraint)
            => requirement.WithMustType(MustType.NotEqual).WithConstraint(constraint);

        public static void Owner(this MustRequirement configuration)
            => configuration.WithMustType(MustType.Owner);
    }
}