using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEmailSender
    {
        Task<MimeMessage> CreateAsync(string title, string htmlBody, string[] recepients, object[] attachments);
        Task SendAsync(MimeMessage emailMessage);
        Task CreateSendAsync(string subject, string htmlBody, IList<string> recepients, object[] attachments = null);
    }
}
