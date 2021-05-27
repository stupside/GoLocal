using System;

namespace GoLocal.Bus.Authorizer.Authorizers.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AuthorizedEntityIdAttribute : Attribute
    {
    }
}