using SenderService.Email.Contracts;
using SenderService.Email.EF.Contracts;
using SenderService.Email.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.EF
{
    public class EFEmailSender : EmailSender, IEFEmailSender
    {
        new protected readonly IEFTemplateProvider _templateProvider;
        new protected readonly IEFSendingOptionsProvider _sendingOptionsProvider;

        public EFEmailSender(IEFTemplateProvider templateProvider, IEFSendingOptionsProvider sendingOptionsProvider) 
            : base(templateProvider, sendingOptionsProvider)
        {
            _templateProvider = templateProvider;
            _sendingOptionsProvider = sendingOptionsProvider;
        }
       
        public virtual async Task<Template> GetTemplateAsync(int id)
        {
            Template template = await _templateProvider.GetTemplateAsync(id);
            return template;
        }

        public virtual async Task SetSendingOptionsAsync(int id)
        {
            SendingOption sendingOptions = await _sendingOptionsProvider.GetSendingOptionsAsync(id);
            SetSendingOptions(sendingOptions);
        }
    }
}
