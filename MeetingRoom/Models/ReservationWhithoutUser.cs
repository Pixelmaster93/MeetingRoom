namespace MeetingRoom.Models
{
    public class ReservationWhithoutUser
    {
        public int Id { get; set; }
        public DateOnly Date { get ; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set;}
        
        public int RoomId { get; set; }

    }
}
