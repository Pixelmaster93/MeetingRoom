namespace MeetingRoom.Services
{
    public interface IMailService
    {
        void HostSend(string subject, string message);
        void CustomerSend(string subject, string message, string customerMail);

    }
}
