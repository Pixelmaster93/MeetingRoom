using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingRoom.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public string Description { get; set; }

        public int NumbersOfReservation
        {
            get
            {
                return Reservations.Count;
            }
        }


        public ICollection<Reservation> Reservations { get; set; }

        public Room(string name)
        {
            Name = name;
        }
    }
}
