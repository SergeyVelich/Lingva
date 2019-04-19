using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEmailTemplateProvider
    {
        Task<string> GetTemplateFromEFAsync(ITemplateSource templateSource, int id);
        Task<string> GetTemplateFromIOAsync(string path, string nameTemplate);
    }
}
