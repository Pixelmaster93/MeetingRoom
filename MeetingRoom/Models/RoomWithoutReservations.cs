namespace MeetingRoom.Models
{
    public class RoomWithoutReservations
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Seats { get; set; }
        public string? Description { get; set; }
    }
}
