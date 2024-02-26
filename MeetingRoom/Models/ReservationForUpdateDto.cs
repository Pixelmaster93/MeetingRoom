namespace MeetingRoom.Models
{
    public class ReservationForUpdateDto
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
