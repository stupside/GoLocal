using Microsoft.EntityFrameworkCore;

namespace GoLocal.Identity.Infrastructure.Persistence.EntityFramework
{
    public class OidcContext : DbContext
    {
        public OidcContext(DbContextOptions<OidcContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}