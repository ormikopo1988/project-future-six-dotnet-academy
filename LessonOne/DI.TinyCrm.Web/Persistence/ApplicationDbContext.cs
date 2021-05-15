using DI.TinyCrm.Web.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DI.TinyCrm.Web.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { set; get; }

        public DbSet<Product> Products { set; get; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
