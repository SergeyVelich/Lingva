using SenderService.Email.EF.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SenderService.Email.Entities
{
    [ExcludeFromCodeCoverage]
    public class Template
    {
        public string Text { get; set; }
        public string[] Parameters { get; set; }

        public Template()
        {

        }

        public Template(EmailTemplate emailTemplate)
        {
            Text = emailTemplate.Text;
            Parameters = emailTemplate.Parameters;
        }
    }
}
