using SenderService.SettingsProvider.Core.Contracts;
using SenderService.SettingsProvider.Core.Entities;
using System.Threading.Tasks;

namespace SenderService.SettingsProvider.Core.Providers
{
    public class EmailSettingsProvider : IEmailSettingsProvider
    {
        private readonly IRepository _repository;

        public EmailSettingsProvider(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailSettings> GetSettingsAsync(int id)
        {
            return await _repository.GetByIdAsync<EmailSettings>(id);
        }
    }
}
