using MailKit.Net.Smtp;
using MimeKit;
namespace ShopOnline.Helper
{
    public class EmailHelper
    {
        public static void SendMail(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ShopOnline", "*****@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };
        }
    }
}
