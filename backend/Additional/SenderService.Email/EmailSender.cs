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
        public virtual async Task<MimeMessage> CreateAsync(string subject, string htmlBody, string[] recepients, object[] attachments)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("From no reply", "noReplyAdministration@gmail.com"));
            emailMessage.Date = DateTime.UtcNow;
            foreach (string recepient in recepients)
            {
                emailMessage.To.Add(new MailboxAddress("", recepient));
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
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("faweradf@gmail.com", "Q!w2e3r4");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
        public virtual async Task CreateSendAsync(string title, string htmlBody, string[] recepients, object[] attachments)
        {
            MimeMessage emailMessage = await CreateAsync(title, htmlBody, recepients, attachments);
            await SendAsync(emailMessage);
        }
    }
}
