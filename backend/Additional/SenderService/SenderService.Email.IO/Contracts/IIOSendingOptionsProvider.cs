using SenderService.Email.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IIOSendingOptionsProvider : IEmailSendingOptionsProvider
    {
        Task<SendingOption> GetSendingOptionsAsync(string path, string fileName);
    }
}
