using GoLocal.Identity.Application.Persistence;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Identity.Infrastructure.Persistence.EntityFramework
{
    public class Context : IdentityDbContext<User, Role, string>, IContext
    {
        public Context(DbContextOptions<Context> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}