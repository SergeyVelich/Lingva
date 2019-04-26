using MimeKit;
using SenderService.Email.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEmailSender
    {
        MimeMessage Create(string title, string htmlBody, IList<string> recepients);
        Task SendAsync(MimeMessage emailMessage);
        Task CreateSendAsync(string subject, string htmlBody, IList<string> recepients);

        void SetSendingOptions(SendingOption sendingOptions);
    }
}
