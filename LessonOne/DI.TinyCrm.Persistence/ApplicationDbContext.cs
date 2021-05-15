using DI.TinyCrm.Core.Entities;
using DI.TinyCrm.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace DI.TinyCrm.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
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

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
