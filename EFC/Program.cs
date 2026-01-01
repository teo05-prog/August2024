using EFC;
using EFC.Entities;

var context = new HotelContext();

Console.WriteLine("Database and tables created successfully.");

var dataAccess = new DataAccess();

Console.WriteLine("=== Hotel Database ===\n");

// Add Rooms
Console.WriteLine("--- Adding Rooms ---");
var room101 = new Room
{
    RoomNumber = 1,
    Theme = "Ocean View",
    NumberOfBeds = 2,
    HasSpa = true,
    FloorNumber = 1,
    PricePerNight = 150f
};
await dataAccess.CreateRoomAsync(room101);
Console.WriteLine($"Added: Room 101 (Beds: {room101.NumberOfBeds}, Spa: {room101.HasSpa}, Price: {room101.PricePerNight})");

var room102 = new Room
{
    RoomNumber = 2,
    Theme = "Ocean View",
    NumberOfBeds = 1,
    HasSpa = false,
    FloorNumber = 1,
    PricePerNight = 80f
};
await dataAccess.CreateRoomAsync(room102);
Console.WriteLine($"Added: Room 102 (Beds: {room102.NumberOfBeds}, Spa: {room102.HasSpa}, Price: {room102.PricePerNight})");

var room201 = new Room
{
    RoomNumber = 1,
    Theme = "Mountain View",
    FloorNumber = 2,
    NumberOfBeds = 3,
    HasSpa = true,
    PricePerNight = 200f
};
await dataAccess.CreateRoomAsync(room201);
Console.WriteLine($"Added: Room 201 (Beds: {room201.NumberOfBeds}, Spa: {room201.HasSpa}, Price: {room201.PricePerNight})");

Console.WriteLine();

// Add Reservations to Rooms
Console.WriteLine("--- Adding Reservations to Rooms ---");

var reservation1 = new Reservation
{
    GuestName = "John Doe",
    CheckInDate = new DateTime(2024, 7, 1),
    NumberOfNights = 3,
    HasBreakfast = true
};
await dataAccess.AddReservationToRoomAsync(room101.RoomId, reservation1);
Console.WriteLine($"Added Reservation for {reservation1.GuestName} to Room 101");

var reservation2 = new Reservation
{
    GuestName = "Jane Smith",
    CheckInDate = new DateTime(2024, 7, 5),
    NumberOfNights = 2,
    HasBreakfast = false
};
await dataAccess.AddReservationToRoomAsync(room102.RoomId, reservation2);
Console.WriteLine($"Added Reservation for {reservation2.GuestName} to Room 102");

var reservation3 = new Reservation
{
    GuestName = "Alice Johnson",
    CheckInDate = new DateTime(2024, 7, 10),
    NumberOfNights = 4,
    HasBreakfast = true
};
await dataAccess.AddReservationToRoomAsync(room201.RoomId, reservation3);
Console.WriteLine($"Added Reservation for {reservation3.GuestName} to Room 201");

Console.WriteLine();

// Get Rooms with specific criteria
Console.WriteLine("--- Getting Rooms with 2 Beds and Spa ---");
var roomsWithSpa = await dataAccess.GetRoomsAsync(2, true);
foreach (var room in roomsWithSpa)
{
    Console.WriteLine($"RoomId: {room.RoomId}, Beds: {room.NumberOfBeds}, Spa: {room.HasSpa}, Price: {room.PricePerNight}");
}

Console.WriteLine();

// Get Earnings between dates
Console.WriteLine("--- Getting Earnings between 2024-07-01 and 2024-07-15 ---");
var earnings = await dataAccess.GetEarningsBetweenAsync(
    new DateTime(2024, 7, 1),
    new DateTime(2024, 7, 15));
Console.WriteLine($"Total Earnings: {earnings}");

Console.WriteLine("\n=== End of Hotel Database Operations ===");