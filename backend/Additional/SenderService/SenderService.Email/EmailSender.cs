using MailKit.Net.Smtp;
using MimeKit;
using SenderService.Email.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenderService.Email
{
    public class EmailSender : IEmailSender
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public EmailSender()
        {

        }

        public virtual MimeMessage Create(string subject, string htmlBody, IList<string> recepients)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("From no reply", "noReplyAdministration@gmail.com"));
            emailMessage.Date = DateTime.UtcNow;
            foreach (string recepient in recepients)
            {
                emailMessage.To.Add(new MailboxAddress(recepient, recepient));
            }
            
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlBody
            };

            return emailMessage;
        }
        public virtual async Task SendAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(Host, Port, UseSsl);
                await client.AuthenticateAsync(UserName, Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
        public virtual async Task CreateSendAsync(string subject, string htmlBody, IList<string> recepients)
        {
            MimeMessage emailMessage = Create(subject, htmlBody, recepients);
            await SendAsync(emailMessage);
        }
    }
}
