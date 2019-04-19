using MimeKit;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEmailMessageSender
    {
        Task SendAsync(MimeMessage emailMessage);
    }
}
