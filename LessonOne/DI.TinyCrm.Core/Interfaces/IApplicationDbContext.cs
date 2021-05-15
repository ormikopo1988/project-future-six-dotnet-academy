using DI.TinyCrm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DI.TinyCrm.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Customer> Customers { set; get; }

        public DbSet<Product> Products { set; get; }

        Task<int> SaveChangesAsync();
    }
}
