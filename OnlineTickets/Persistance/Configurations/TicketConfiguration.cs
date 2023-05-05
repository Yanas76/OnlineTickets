using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineTickets.Entities;

namespace OnlineTickets.Persistance.Configurations;

public class TicketConfiguration: IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Film).
        WithMany(x => x.Tickets).
        HasForeignKey(x => x.FilmId).
        OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Order).
        WithMany(x => x.Tickets).
        HasForeignKey(x => x.OrderId).
        OnDelete(DeleteBehavior.Cascade);

    }
}
