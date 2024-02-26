using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MeetingRoom.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MailAddres { get; set; }
        public string PhoneNumber { get; set; }

        public int NumbersOfReservation
        {
            get
            {
                return Reservations.Count;
            }
        }

        
        public ICollection<Reservation> Reservations { get; set; }
           = new List<Reservation>();

        public User(string userName)
        {
            UserName = userName;
        }
    }
}
