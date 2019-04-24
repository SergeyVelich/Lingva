using SenderService.Email.Contracts;
using SenderService.Email.EF.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Providers
{
    public class EFSendingOptionsProvider : IEFSendingOptionsProvider
    {
        private readonly IEmailSenderRepository _repository;

        public EFSendingOptionsProvider(IEmailSenderRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailSendingOption> GetSendingOptionsAsync(int id)
        {
            EmailSendingOption sendingOption = await _repository.GetByIdAsync<EmailSendingOption>(id);
            return sendingOption;
        }
    }
}
