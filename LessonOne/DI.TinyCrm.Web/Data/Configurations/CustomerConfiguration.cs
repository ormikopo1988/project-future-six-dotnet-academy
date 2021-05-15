using DI.TinyCrm.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DI.TinyCrm.Web.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.Property(cus => cus.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(cus => cus.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(cus => cus.VatNumber)
                .IsRequired()
                .HasMaxLength(9);
            builder.Property(cus => cus.Address)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
