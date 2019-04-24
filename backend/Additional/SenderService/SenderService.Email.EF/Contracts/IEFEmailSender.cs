using SenderService.Email.Contracts;
using SenderService.Email.EF.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Contracts
{
    public interface IEFEmailSender : IEmailSender
    {
        Task<EmailTemplate> GetTemplateAsync(int id);

        Task SetSendingOptionsAsync(int id);
    }
}
