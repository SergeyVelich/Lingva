using SenderService.SettingsProvider.Core.Entities;
using System.Threading.Tasks;

namespace SenderService.SettingsProvider.Core.Contracts
{
    public interface IEmailTemplateProvider
    {
        Task<EmailTemplate> GetTemplateAsync(int id);
    }
}
