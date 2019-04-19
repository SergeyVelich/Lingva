using SenderService.Email.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace SenderService.Email
{
    public class EmailTemplateProvider : IEmailTemplateProvider
    {
        public async Task<string> GetTemplateFromEFAsync(ITemplateSource templateSource, int id)
        {
            return await templateSource.GetAsync(id);
        }

        public virtual async Task<string> GetTemplateFromIOAsync(string pathDirectory, string nameTemplate)
        {
            string template;

            string path = Path.Combine(Directory.GetCurrentDirectory(), pathDirectory, nameTemplate);

            using (var reader = File.OpenText(path))
            {
                template = await reader.ReadToEndAsync();
            }

            return template;
        }
    }
}
