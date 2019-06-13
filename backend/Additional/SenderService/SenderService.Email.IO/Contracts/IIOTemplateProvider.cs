using SenderService.Email.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IIOTemplateProvider : IEmailTemplateProvider
    {
        Task<Template> GetTemplateAsync(string path, string fileName);
    }
}
