using DI.TinyCrm.Web.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DI.TinyCrm.Web.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.Property(pro => pro.Code)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(bd => bd.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(bd => bd.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(bd => bd.Price)
                .IsRequired();
            builder.Property(bd => bd.Quantity)
                .IsRequired();
        }
    }
}
