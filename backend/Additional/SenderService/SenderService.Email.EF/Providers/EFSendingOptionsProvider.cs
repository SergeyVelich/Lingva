using SenderService.Email.Contracts;
using SenderService.Email.EF.DAL.Contracts;
using SenderService.Email.EF.DAL.Entities;
using SenderService.Email.Entities;
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

        public async Task<SendingOption> GetSendingOptionsAsync(int id)
        {
            EmailSendingOption emailSendingOption = await _repository.GetByIdAsync<EmailSendingOption>(id);
            return new SendingOption(emailSendingOption);
        }
    }
}
