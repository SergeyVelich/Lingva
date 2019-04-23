using SenderService.Email.Contracts;
using SenderService.Email.EF.Entities;
using SenderService.Email.EF.Providers;
using System.Threading.Tasks;

namespace SenderService.Email.EF
{
    public class EFEmailSender : EmailSender
    {
        public EFEmailSender(IEmailTemplateProvider templateProvider, IEmailSendingOptionsProvider sendingOptionsProvider) 
            : base(templateProvider, sendingOptionsProvider)
        {

        }
       
        public virtual async Task<EmailTemplate> GetTemplateAsync(int id)
        {
            return await ((EFTemplateProvider)_templateProvider).GetTemplateAsync(id);
        }

        public virtual async Task SetSendingOptionsAsync(int id)
        {
            EmailSendingOption sendingOptions = await ((EFSendingOptionsProvider)_sendingOptionsProvider).GetSendingOptionsAsync(id);
            SetSendingOptions(sendingOptions);
        }
    }
}
