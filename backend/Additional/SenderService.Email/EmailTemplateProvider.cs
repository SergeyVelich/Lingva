﻿using Lingva.DAL.Entities;
using SenderService.Email.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace SenderService.Email
{
    public class EmailTemplateProvider : IEmailTemplateProvider
    {
        public virtual async Task<EmailTemplate> GetTemplateAsync(ITemplateSource templateSource, int id)
        {
            return await templateSource.GetTemplateAsync(id);
        }

        public virtual async Task<EmailTemplate> GetTemplateAsync(string pathDirectory, string nameTemplate)
        {
            string template;

            string path = Path.Combine(Directory.GetCurrentDirectory(), pathDirectory, nameTemplate);

            using (var reader = File.OpenText(path))
            {
                template = await reader.ReadToEndAsync();
            }

            return null;
        }
    }
}
