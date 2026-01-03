using EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFC;

public class DataAccess
{
    private readonly HotelContext context;

    public DataAccess()
    {
        context = new HotelContext();
    }

    public async Task CreateRoomAsync(Room room)
    {
        context.Rooms.Add(room);
        await context.SaveChangesAsync();
    }

    public async Task AddReservationToRoomAsync(int roomId,
        Reservation reservation)
    {
        var room = await context.Rooms.FindAsync(roomId);
        if (room != null)
        {
            reservation.RoomId = roomId;
            context.Reservations.Add(reservation);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Room>> GetRoomsAsync(int numberOfBeds, bool hasSpa)
    {
        return await context.Rooms
            .Where(r => r.NumberOfBeds == numberOfBeds && r.HasSpa == hasSpa)
            .ToListAsync();
    }

    public async Task<float> GetEarningsBetweenAsync(DateOnly startDate,
        DateOnly endDate)
    {
        var reservations = await context.Reservations
            .Include(r => r.Room)
            .Where(r => r.CheckInDate >= startDate &&
                        r.CheckInDate.AddDays(r.NumberOfNights) <= endDate)
            .ToListAsync();

        float totalEarnings = 0;
        foreach (var reservation in reservations)
        {
            totalEarnings += reservation.NumberOfNights *
                             reservation.Room.PricePerNight;
            // breakfast is 20% extra of total price per reservation
            if (reservation.HasBreakfast)
            {
                totalEarnings += reservation.NumberOfNights *
                                 reservation.Room.PricePerNight * 0.2f;
            }
        }
        return totalEarnings;
    }
}