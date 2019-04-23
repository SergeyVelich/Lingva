using SenderService.Email.Contracts;
using SenderService.Email.EF.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Providers
{
    public class EFTemplateProvider : IEmailTemplateProvider
    {
        private readonly IEmailSenderRepository _repository;

        public EFTemplateProvider(IEmailSenderRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailTemplate> GetTemplateAsync(int id)
        {
            EmailTemplate template = await _repository.GetByIdAsync<EmailTemplate>(id);
            return template;
        }
    }
}
