namespace EFC.Entities;

public class Room
{
    public int RoomId { get; set; }
    public string Theme { get; set; }
    public int FloorNumber { get; set; }
    public int RoomNumber { get; set; }
    public float PricePerNight { get; set; }
    public int NumberOfBeds { get; set; }
    public bool HasSpa { get; set; }
}