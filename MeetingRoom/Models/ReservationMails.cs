using MeetingRoom.Entities;

namespace MeetingRoom.Models
{
    public class ReservationMails
    {

        public int Id { get; set; }
        public DateOnly Date { get ; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set;}
        public Room? Room { get; set; } 

        public User? User { get; set; }

        public string? InviteMails { get; set; }

        public int RoomId { get; set; }
        public int UserId { get; set; }
    }
}
