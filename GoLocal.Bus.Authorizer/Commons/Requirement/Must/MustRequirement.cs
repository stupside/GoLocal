using System;
using System.Data;
using System.Linq.Expressions;

namespace GoLocal.Bus.Authorizer.Commons.Requirement.Must
{
    public class MustRequirement : IAuthorizerRequirement
    {
        public MustType MustType { get; private set; }
        private object Constraint { get; set; }
        private Delegate MustDelegate { get; set; }
        
        internal MustRequirement WithIdentifier<TEntity, T>(Expression<Func<TEntity, T>> identifier)
        {
            MustDelegate = identifier.Compile();
            return this;
        }

        internal MustRequirement WithConstraint(object constraint)
        {
            Constraint = constraint;
            return this;
        }
        
        internal MustRequirement WithMustType(MustType type)
        {
            MustType = type;
            return this;
        }

        internal bool IsValidConstraint(object entity, object constraint)
        {
            if (constraint == null)
                throw new InvalidConstraintException("Constraint cannot be null");

            var value = MustDelegate.DynamicInvoke(entity);

            return MustType switch
            {
                MustType.Equal => value?.Equals(constraint) ?? false,
                MustType.NotEqual => !value?.Equals(constraint) ?? false,
                MustType.Owner => value?.Equals(constraint) ?? false,
                _ => false
            };
        }

        public bool IsValidConstraint(object entity)
            => IsValidConstraint(entity, Constraint);
    }
}

