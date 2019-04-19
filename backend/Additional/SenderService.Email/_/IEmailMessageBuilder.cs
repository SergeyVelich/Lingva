using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface IEmailMessageBuilder
    {
        Task<MimeMessage> CreateAsync(string text, string title, string toEmail, params KeyValuePair<string, string>[] replacers);
    }
}
