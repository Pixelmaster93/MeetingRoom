using System.Net.Mail;

namespace MeetingRoom.Services
{
    public class MailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;


        public MailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                Subject = subject,
                Body = message,
                From = new MailAddress(_mailFrom),
            };

            mailMessage.To.Add(_mailTo);

            var client = new SmtpClient
            {
                Host = "gty",
                Port = 587,
                Credentials = new System.Net.NetworkCredential(),
            };

            client.Send(mailMessage);
            /*
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " +
                $"with {nameof(MailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
            */
        }

        public void CustomerSend(string subject, string message, string customerMail) 
        {
            var mailMessage = new MailMessage
            {
                Subject = subject,
                Body = message,
                From = new MailAddress(_mailFrom),
            };

            mailMessage.To.Add(customerMail);

            var client = new SmtpClient
            {
                Host = "gty",
                Port = 587,
                Credentials = new System.Net.NetworkCredential(),
            };

            client.Send(mailMessage);

            /*
            Console.WriteLine($"Mail from {_mailFrom} to {customerMail}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
            */
        }

    }
}
