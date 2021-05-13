using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Abstracts;
using GoLocal.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Persistence.EntityFramework
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShopCategory> ShopCategories { get; set; }

        public DbSet<Opening> Openings { get; set; }
        
        public DbSet<Item> Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        
        public DbSet<Package> Packages { get; set; }

        public DbSet<Command> Commands { get; set; }
        public DbSet<Message> Messages { get; set; }
        
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartPackage> CartPackages { get; set; }
        
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        
        public DbSet<Comment> Comments { get; set; }

        public Context(DbContextOptions<Context> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}