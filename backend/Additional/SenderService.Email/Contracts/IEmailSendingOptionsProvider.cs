using Lingva.DAL.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEmailSendingOptionsProvider
    {
        Task<EmailSendingOption> GetSendingOptionsAsync(ISendingOptionsSource templateSource, int id);
        Task<EmailSendingOption> GetSendingOptionsAsync(string path, string nameTemplate);
    }
}
