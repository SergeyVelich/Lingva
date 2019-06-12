using SenderService.SettingsProvider.Core.Entities;
using System.Threading.Tasks;

namespace SenderService.SettingsProvider.Core.Contracts
{
    public interface IEmailSettingsProvider
    {
        Task<EmailSettings> GetSettingsAsync(int id);
    }
}
