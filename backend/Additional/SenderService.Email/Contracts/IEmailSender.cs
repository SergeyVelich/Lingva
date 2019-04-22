using Lingva.DAL.Entities;
using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEmailSender : IEmailTemplateProvider
    {
        MimeMessage Create(string title, string htmlBody, IList<string> recepients);
        Task SendAsync(MimeMessage emailMessage);
        Task CreateSendAsync(string subject, string htmlBody, IList<string> recepients);

        Task SetSendingOptionsAsync(ISendingOptionsSource templateSource, int id);
        Task SetSendingOptionsAsync(string path, string nameTemplate);
        void SetSendingOptions(EmailSendingOption sendingOptions);
    }
}
