using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AspNetErandrosTools.Extensions;

namespace AspNetErandrosTools.Services
{
    public class EmailSettings
    {
        public string Server { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }

    public class Emailer
    {
        private readonly EmailSettings EmailSettings;

        public Emailer(EmailSettings emailSettings)
        {
            EmailSettings = emailSettings;
        }

        public Task SendEmailAsync(string to, string subject, string message, string bcc = null)
        {
            // Configure the client:
            SmtpClient client = new System.Net.Mail.SmtpClient(EmailSettings.Server);

            client.Port = EmailSettings.Port;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(
                EmailSettings.UserName, EmailSettings.Password);

            client.EnableSsl = EmailSettings.EnableSsl;
            client.Credentials = credentials;

            // Create the message:
            var mail = new System.Net.Mail.MailMessage(
                new MailAddress(EmailSettings.UserName, EmailSettings.DisplayName),
                new MailAddress(to, to));

            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = message;

            // Send:
            return client.SendMailAsync(mail);
        }
    }
}
