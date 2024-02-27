using MeetingRoom.Entities;

namespace MeetingRoom.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Seats { get; set; }
        public string? Description { get; set; }
       
        
        public int? NumberOfReservations 
        {
            get
            {
                return Reservations.Count;
            }
        }


        public ICollection<ReservationWhithoutUsersRooms> Reservations { get; set; }
            = new List<ReservationWhithoutUsersRooms>();

    }
}
