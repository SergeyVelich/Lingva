using SenderService.Email.Contracts;
using SenderService.Email.EF.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Contracts
{
    public interface IIOEmailSender : IEmailSender
    {
        Task<EmailTemplate> GetTemplateAsync(string path, string fileName);

        Task SetSendingOptionsAsync(string path, string fileName);
    }
}
