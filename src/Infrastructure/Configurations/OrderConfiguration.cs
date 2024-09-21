using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Number)
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(x => x.SaleDate)
            .IsRequired();

        builder.OwnsOne(x => x.Amount, amount =>
        {
            amount.Property(x => x.ValueInCents)
                .IsRequired()
                .HasColumnName("Amount");
        });
        
        builder.OwnsOne(x => x.Status, status =>
        {
            status.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("Status");
        });

        builder.Property(x => x.IsCanceled)
            .IsRequired();

        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.MerchantId)
            .IsRequired();

        builder.HasMany(x => x.StatusHistory)
            .WithOne()
            .HasForeignKey(x => x.OrderId);
    }
}