using SenderService.Email.EF.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEFTemplateProvider : IEmailTemplateProvider
    {
        Task<EmailTemplate> GetTemplateAsync(int id);
    }
}
