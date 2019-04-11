using System;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace WebApplication1.Controllers.extends
{
    public class SendMailKitEmail
    {
        public static string sendEmail(string email, string name, string mailSubject, string mailBody)
        {
            string fromName = "zhaoyangkanshijie";
            string fromEmail = "zhaoyangkanshijie@zhaoyangkanshijie.com.cn";
            string mailClient = "smtp.zhaoyangkanshijie.com.cn";
            string mailAccount = "zhaoyangkanshijie@zhaoyangkanshijie.com.cn";
            string mailPassword = "zhaoyangkanshijie";
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, fromEmail));
                message.To.Add(new MailboxAddress(name, email));
                message.Subject = mailSubject;

                message.Body = new TextPart("html")
                {
                    Text = mailBody
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(mailClient, 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(mailAccount, mailPassword);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
            return "success";
        }
    }
}