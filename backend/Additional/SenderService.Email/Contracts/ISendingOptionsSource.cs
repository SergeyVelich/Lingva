using Lingva.DAL.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface ISendingOptionsSource
    {
        Task<EmailSendingOption> GetSendingOptionsAsync(int id);
    }
}
