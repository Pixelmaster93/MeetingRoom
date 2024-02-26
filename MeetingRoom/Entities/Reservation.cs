using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MeetingRoom.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }


    }
}
