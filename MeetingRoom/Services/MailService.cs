using System.Net;
using System.Net.Mail;

namespace MeetingRoom.Services
{
    public class MailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;
        private readonly string _userSMTP = string.Empty;
        private readonly string _passSMTP = string.Empty;
        private readonly string _urlSMTP = string.Empty;
        private readonly int _portSMTP;


        public MailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];
            _userSMTP = configuration["smtpCredential:user"];
            _passSMTP = configuration["smtpCredential:password"];
            _urlSMTP = configuration["smtpConfig:url"];
            _portSMTP =int.Parse(configuration["smtpConfig:port"]);
        }

        public void HostSend(string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailFrom),
                Subject = subject,
                Body = message,
            };

            mailMessage.To.Add(_mailTo);

            var client = new SmtpClient(_urlSMTP, _portSMTP)
            {
                Credentials = new NetworkCredential(_userSMTP, _passSMTP),
                EnableSsl = true
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
            var mailHost = new MailMessage
            {
                From = new MailAddress(_mailFrom),
                Subject = subject,
                Body = message,
            };

            mailHost.To.Add(customerMail);

            var mailClient = new SmtpClient(_urlSMTP, _portSMTP)
            {
                Credentials = new NetworkCredential(_userSMTP, _passSMTP),
                EnableSsl = true
            };

            mailClient.Send(mailHost);

            /*
            Console.WriteLine($"Mail from {_mailFrom} to {customerMail}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
            */
        }

    }
}
