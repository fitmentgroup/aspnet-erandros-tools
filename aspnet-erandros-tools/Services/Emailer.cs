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
        private readonly IConfiguration _config;
        private readonly EmailSettings _settings;

        public IHostingEnvironment Env { get; private set; }

        public Emailer(IConfiguration config, IHostingEnvironment env)
        {
            Env = env;
            _config = config;
            _settings = new EmailSettings()
            {
                Server = _config["Data:EmailSettings:Server"],
                EnableSsl = Convert.ToBoolean(_config["Data:EmailSettings:EnableSsl"]),
                Port = Convert.ToInt16(_config["Data:EmailSettings:Port"]),
                UserName = _config["Data:EmailSettings:UserName"],
                Password = _config["Data:EmailSettings:Password"],
                DisplayName = _config["Data:EmailSettings:DisplayName"],
            };
        }

        public Task SendEmailAsync(string to, string subject, string message, string bcc = null)
        {
            // Configure the client:
            SmtpClient client = new System.Net.Mail.SmtpClient(_settings.Server);

            client.Port = _settings.Port;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(
                _settings.UserName, _settings.Password);

            client.EnableSsl = _settings.EnableSsl;
            client.Credentials = credentials;

            // Create the message:
            var mail = new System.Net.Mail.MailMessage(
                new MailAddress(_settings.UserName, _settings.DisplayName),
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
