using OnlineTickets.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Dynamic;

namespace OnlineTickets.Persistance.Configurations;

public class FilmConfiguration: IEntityTypeConfiguration<Film>
{
    public void Configure(EntityTypeBuilder<Film> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Cinema).
          WithMany(x => x.Films).
          HasForeignKey(x => x.CinemaId).
          OnDelete(DeleteBehavior.Cascade);
    }
}
