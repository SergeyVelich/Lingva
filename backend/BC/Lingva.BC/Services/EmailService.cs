using Lingva.BC.Contracts;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using SenderService.Email.Contracts;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class EmailService : IEmailService, ITemplateSource, ISendingOptionsSource
    {
        private readonly IRepository _repository;
           
        public EmailService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailTemplate> GetTemplateAsync(int id)
        {
            EmailTemplate template = await _repository.GetByIdAsync<EmailTemplate>(id);
            return template;
        }

        public async Task<EmailSendingOption> GetSendingOptionsAsync(int id)
        {
            EmailSendingOption sendingOption = await _repository.GetByIdAsync<EmailSendingOption>(id);
            return sendingOption;
        }
    }
}

