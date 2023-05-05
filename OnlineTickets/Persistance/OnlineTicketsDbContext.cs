using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineTickets.Entities;
using OnlineTickets.Persistance.Configurations;

namespace OnlineTickets.Persistance;

public class OnlineTicketsDbContext: DbContext
{
    public OnlineTicketsDbContext(DbContextOptions<OnlineTicketsDbContext> options): base(options)
    {       
        Database.EnsureCreated();
    }        

    public DbSet<User> Users { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new CinemaConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new FilmConfiguration());
        
    }
}
