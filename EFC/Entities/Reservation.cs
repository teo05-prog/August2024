namespace EFC.Entities;

public class Reservation
{
    public int ReservationId { get; set; }
    public string GuestName { get; set; }
    public DateOnly CheckInDate { get; set; }
    public int NumberOfNights { get; set; }
    public bool HasBreakfast { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
}