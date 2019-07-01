using SenderService.SettingsProvider.Core.Contracts;
using SenderService.SettingsProvider.Core.Entities;
using System.Threading.Tasks;

namespace SenderService.SettingsProvider.Core.Providers
{
    public class EmailTemplateProvider : IEmailTemplateProvider
    {
        private readonly IRepository _repository;

        public EmailTemplateProvider(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailTemplate> GetTemplateAsync(int id)
        {
            return await _repository.GetByIdAsync<EmailTemplate>(id);
        }
    }
}
