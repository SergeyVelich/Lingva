using Lingva.DAL.Entities;
using SenderService.Email.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace SenderService.Email
{
    public class EmailSendingOptionsProvider : IEmailSendingOptionsProvider
    {
        public virtual async Task<EmailSendingOption> GetSendingOptionsAsync(ISendingOptionsSource sendingOptionsSource, int id)
        {
            return await sendingOptionsSource.GetSendingOptionsAsync(id);
        }

        public virtual async Task<EmailSendingOption> GetSendingOptionsAsync(string pathDirectory, string nameTemplate)
        {
            string template;

            string path = Path.Combine(Directory.GetCurrentDirectory(), pathDirectory, nameTemplate);

            using (var reader = File.OpenText(path))
            {
                template = await reader.ReadToEndAsync();
            }

            return null;//??
        }
    }
}
