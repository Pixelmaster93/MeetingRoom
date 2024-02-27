namespace MeetingRoom.Models
{
    public class ReservationWithNames
    {
        public DateOnly Date { get ; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set;}
        public string UserName { get; set; }
        public string RoomName { get; set; }

        
    }
}
