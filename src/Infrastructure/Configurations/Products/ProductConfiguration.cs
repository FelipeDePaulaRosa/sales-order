using Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Code)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Brand)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Active)
            .IsRequired();

        builder.OwnsOne(x => x.UnitPrice, up =>
        {
            up.Property(x => x.ValueInCents)
                .HasColumnName("UnitPrice")
                .IsRequired();

            up.Ignore(x => x.Value);
        });
        
        builder.OwnsOne(x => x.Discount, d =>
        {
            d.Property(x => x.ValueInPercentage)
                .HasColumnName("Discount")
                .IsRequired();
        });
        
        builder.Property(x => x.Stock)
            .IsRequired();
    }
}