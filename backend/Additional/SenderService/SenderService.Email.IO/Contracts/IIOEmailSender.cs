using SenderService.Email.Contracts;
using SenderService.Email.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Contracts
{
    public interface IIOEmailSender : IEmailSender
    {
        Task<Template> GetTemplateAsync(string path, string fileName);

        Task SetSendingOptionsAsync(string path, string fileName);
    }
}
