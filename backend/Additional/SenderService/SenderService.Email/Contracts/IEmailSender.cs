using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEmailSender
    {
        string Host { get; set; }
        int Port { get; set; }
        bool UseSsl { get; set; }
        string UserName { get; set; }
        string Password { get; set; }

        MimeMessage Create(string title, string htmlBody, IList<string> recepients);
        Task SendAsync(MimeMessage emailMessage);
        Task CreateSendAsync(string subject, string htmlBody, IList<string> recepients);        
    }
}
