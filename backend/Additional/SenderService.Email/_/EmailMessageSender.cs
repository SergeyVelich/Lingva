using MailKit.Net.Smtp;
using MimeKit;
using SenderService.Email.Contracts;
using System.Threading.Tasks;

namespace SenderService.Email
{
    public class EmailMessageSender : IEmailMessageSender
    {
        public async Task SendAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("faweradf@gmail.com", "Q!w2e3r4");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
