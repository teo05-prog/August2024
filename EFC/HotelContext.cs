using Microsoft.EntityFrameworkCore;

namespace EFC;

public class HotelContext : DbContext
{
    public DbSet<Entities.Room> Rooms { get; set; }
    public DbSet<Entities.Reservation> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=hotel.db");
    }
}