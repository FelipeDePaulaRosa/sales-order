using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
{
    public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
    {
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