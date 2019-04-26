using SenderService.Email.Contracts;
using SenderService.Email.Entities;
using System.IO;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Providers
{
    public class IOTemplateProvider : IIOTemplateProvider
    {
        public async Task<Template> GetTemplateAsync(string directoryPath, string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), directoryPath, fileName);

            Template template = new Template();

            using (var reader = File.OpenText(path))
            {
                template.Text = await reader.ReadToEndAsync();
            }

            return template;
        }
    }
}
