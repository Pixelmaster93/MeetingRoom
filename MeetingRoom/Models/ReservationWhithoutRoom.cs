namespace MeetingRoom.Models
{
    public class ReservationWhithoutRoom
    {
        public int Id { get; set; }
        public DateOnly Date { get ; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set;}
        
        public ICollection<UserDto> User { get; set; }
             = new List<UserDto>();

    }
}
