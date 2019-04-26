using SenderService.Email.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEFSendingOptionsProvider : IEmailSendingOptionsProvider
    {
        Task<SendingOption> GetSendingOptionsAsync(int id);
    }
}
