using Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Orders;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.ToTable("OrderProduct");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderId)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.OwnsOne(x => x.UnitPrice, unitPrice =>
        {
            unitPrice.Property(x => x.ValueInCents)
                .HasColumnName("UnitPrice")
                .IsRequired();

            unitPrice.Ignore(x => x.Value);
        });

        builder.OwnsOne(x => x.Discount, discount =>
        {
            discount.Property(x => x.ValueInPercentage)
                .HasColumnName("Discount")
                .IsRequired();
        });
        
        builder.OwnsOne(x => x.Amount, amount =>
        {
            amount.Property(x => x.ValueInCents)
                .HasColumnName("Amount")
                .IsRequired();

            amount.Ignore(x => x.Value);
        });

        builder.Property(x => x.IsCanceled)
            .IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}