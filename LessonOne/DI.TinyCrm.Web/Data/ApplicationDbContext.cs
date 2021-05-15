using DI.TinyCrm.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DI.TinyCrm.Web.Data
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
            base.OnModelCreating(builder);

            builder.Entity<Customer>().ToTable("Customer");
            builder.Entity<Customer>()
                .Property(cus => cus.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Customer>()
                .Property(cus => cus.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Customer>()
                .Property(cus => cus.VatNumber)
                .IsRequired()
                .HasMaxLength(9);
            builder.Entity<Customer>()
                .Property(cus => cus.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Product>().ToTable("Product");
            builder.Entity<Product>()
                .Property(pro => pro.Code)
                .IsRequired()
                .HasMaxLength(10);
            builder.Entity<Product>()
                .Property(bd => bd.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.Entity<Product>()
                .Property(bd => bd.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<Product>()
                .Property(bd => bd.Price)
                .IsRequired();
            builder.Entity<Product>()
                .Property(bd => bd.Quantity)
                .IsRequired();
        }
    }
}
