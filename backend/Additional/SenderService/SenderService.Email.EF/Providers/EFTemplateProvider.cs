using SenderService.Email.Contracts;
using SenderService.Email.EF.DAL.Contracts;
using SenderService.Email.EF.DAL.Entities;
using SenderService.Email.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Providers
{
    public class EFTemplateProvider : IEFTemplateProvider
    {
        private readonly IEmailSenderRepository _repository;

        public EFTemplateProvider(IEmailSenderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Template> GetTemplateAsync(int id)
        {
            EmailTemplate emailTemplate = await _repository.GetByIdAsync<EmailTemplate>(id);
            return new Template(emailTemplate);
        }
    }
}
