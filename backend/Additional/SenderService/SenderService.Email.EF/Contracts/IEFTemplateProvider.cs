using SenderService.Email.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEFTemplateProvider : IEmailTemplateProvider
    {
        Task<Template> GetTemplateAsync(int id);
    }
}
