using SenderService.Email.EF.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IIOSendingOptionsProvider : IEmailSendingOptionsProvider
    {
        Task<EmailSendingOption> GetSendingOptionsAsync(string path, string fileName);
    }
}
