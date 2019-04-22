using Lingva.DAL.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEmailTemplateProvider
    {
        Task<EmailTemplate> GetTemplateAsync(ITemplateSource templateSource, int id);
        Task<EmailTemplate> GetTemplateAsync(string path, string nameTemplate);
    }
}
