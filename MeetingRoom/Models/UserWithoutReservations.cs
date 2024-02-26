namespace MeetingRoom.Models
{
    public class UserWithoutReservations
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string MailAddres { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        
    }
}
