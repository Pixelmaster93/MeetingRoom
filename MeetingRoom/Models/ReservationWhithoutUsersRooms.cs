namespace MeetingRoom.Models
{
    public class ReservationWhithoutUsersRooms
    {
        public int Id { get; set; }
        public DateOnly Date { get ; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set;}

        /*
        public int NumbersOfUsers
        {
            get
            {
                return Users.Count;
            }
        }
        
        public ICollection<UserDto> Users { get; set; }
             = new List<UserDto>();
        */
    }
}
