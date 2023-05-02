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

        builder.HasOne(x => x.Film).
           WithMany(x => x.Orders).
           HasForeignKey(x => x.FilmId).
           OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Card).
           WithMany(x => x.Orders).
           HasForeignKey(x => x.CardId).
           OnDelete(DeleteBehavior.Cascade);
    }
}
