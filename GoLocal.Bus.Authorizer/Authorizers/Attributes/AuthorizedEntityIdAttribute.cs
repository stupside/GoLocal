using System;

namespace GoLocal.Bus.Authorizer.Authorizers.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AuthorizedEntityAttribute : Attribute
    {
        public Type Type { get; }

        public AuthorizedEntityAttribute(Type type)
        {
            Type = type;
        }
    }
}