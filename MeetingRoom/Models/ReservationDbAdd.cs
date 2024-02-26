using MeetingRoom.Entities;

namespace MeetingRoom.Models
{
    public class ReservationDbAdd
    {
        public int Id { get; set; }
        public string Date { get ; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set;}

        public int UserId { get; set; } 

        public int RoomId { get; set; } 

    }
}
