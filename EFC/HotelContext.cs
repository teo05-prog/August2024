using Microsoft.EntityFrameworkCore;

namespace EFC;

public class HotelContext : DbContext
{
    public DbSet<Entities.Room> Rooms { get; set; }
    public DbSet<Entities.Reservation> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string projectRoot = Directory.GetCurrentDirectory();
        string dbPath = Path.Combine(projectRoot, "hotel.db");
        
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
        Console.WriteLine($"Database at: {dbPath}");
    }
}