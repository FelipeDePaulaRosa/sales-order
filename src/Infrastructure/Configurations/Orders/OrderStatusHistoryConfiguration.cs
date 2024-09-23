using Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Orders;

public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
{
    public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
    {
        builder.ToTable("OrderStatusHistory");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.OrderId)
            .IsRequired();
        
        builder.Property(x => x.Message)
            .HasMaxLength(300)
            .IsRequired();

        builder.OwnsOne(x => x.Status, status =>
        {
            status.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("Status");
        });

        builder.Property(x => x.CreatedAt);
    }
}