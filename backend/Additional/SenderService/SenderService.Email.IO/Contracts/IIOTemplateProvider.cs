using SenderService.Email.EF.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IIOTemplateProvider : IEmailTemplateProvider
    {
        Task<EmailTemplate> GetTemplateAsync(string path, string fileName);
    }
}
