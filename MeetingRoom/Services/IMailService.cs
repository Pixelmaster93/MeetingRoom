namespace MeetingRoom.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
        void CustomerSend(string subject, string message, string customerMail);

    }
}
