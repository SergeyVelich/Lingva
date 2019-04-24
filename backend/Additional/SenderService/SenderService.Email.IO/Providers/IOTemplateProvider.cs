using SenderService.Email.Contracts;
using SenderService.Email.EF.Entities;
using System.IO;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Providers
{
    public class IOTemplateProvider : IIOTemplateProvider
    {
        public async Task<EmailTemplate> GetTemplateAsync(string directoryPath, string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), directoryPath, fileName);

            EmailTemplate template = new EmailTemplate();

            using (var reader = File.OpenText(path))
            {
                template.Text = await reader.ReadToEndAsync();
            }

            return template;
        }
    }
}
