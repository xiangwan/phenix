using System.Net.Mail;
using System.Text;

namespace Phenix.Infrastructure.Helpers {
    public class MailSender {
        public static void Send(string to, string subject, string body)
        {
            var mm = new MailMessage
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = body,
                BodyEncoding = Encoding.UTF8
            };
            mm.To.Add(to);
            var sc = new SmtpClient();
            sc.Send(mm);
        } 
    }
}