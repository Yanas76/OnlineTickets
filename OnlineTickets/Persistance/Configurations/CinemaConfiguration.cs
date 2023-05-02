using OnlineTickets.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Dynamic;

namespace OnlineTickets.Persistance.Configurations;

public class CinemaConfiguration: IEntityTypeConfiguration<Cinema>
{
    public void Configure(EntityTypeBuilder<Cinema> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Films).
          WithOne(x => x.Cinema).
          HasForeignKey(x => x.CinemaId).
          OnDelete(DeleteBehavior.Cascade);
    }
}
