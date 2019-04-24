using SenderService.Email.EF.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEFSendingOptionsProvider : IEmailSendingOptionsProvider
    {
        Task<EmailSendingOption> GetSendingOptionsAsync(int id);
    }
}
