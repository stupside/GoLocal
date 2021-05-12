using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Accessor.Accessors;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commons.Accessor
{
    public interface IUserAccessor : IUserAccessor<User>
    {
        UserManager<User> Manager { get; }
    }
}