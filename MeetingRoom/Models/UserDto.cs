namespace MeetingRoom.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string MailAddres { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int? NumberOfReservation
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
