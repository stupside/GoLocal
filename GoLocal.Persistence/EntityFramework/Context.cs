using GoLocal.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Persistence.EntityFramework
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}