using OnlineTickets.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Dynamic;

namespace OnlineTickets.Persistance.Configurations;

public class OrderConfiguration: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User).
            WithMany(x => x.Orders).
            HasForeignKey(x => x.UserId).
            OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Tickets).
            WithOne(x => x.Order).
            HasForeignKey(x => x.OrderId).
            OnDelete(DeleteBehavior.Cascade);
    }
}
