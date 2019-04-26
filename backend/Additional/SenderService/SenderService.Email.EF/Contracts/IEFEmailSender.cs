using SenderService.Email.Contracts;
using SenderService.Email.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Contracts
{
    public interface IEFEmailSender : IEmailSender
    {
        Task<Template> GetTemplateAsync(int id);

        Task SetSendingOptionsAsync(int id);
    }
}
