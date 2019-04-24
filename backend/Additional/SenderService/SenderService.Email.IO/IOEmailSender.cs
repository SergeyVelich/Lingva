using SenderService.Email.Contracts;
using SenderService.Email.EF.Contracts;
using SenderService.Email.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.EF
{
    public class IOEmailSender : EmailSender, IIOEmailSender
    {
        new protected readonly IIOTemplateProvider _templateProvider;
        new protected readonly IIOSendingOptionsProvider _sendingOptionsProvider;

        public IOEmailSender(IIOTemplateProvider templateProvider, IIOSendingOptionsProvider sendingOptionsProvider) 
            : base(templateProvider, sendingOptionsProvider)
        {
            _templateProvider = templateProvider;
            _sendingOptionsProvider = sendingOptionsProvider;
        }
       
        public virtual async Task<Template> GetTemplateAsync(string path, string fileName)
        {
            return await _templateProvider.GetTemplateAsync(path, fileName);
        }

        public virtual async Task SetSendingOptionsAsync(string path, string fileName)
        {
            SendingOption sendingOptions = await _sendingOptionsProvider.GetSendingOptionsAsync(path, fileName);
            SetSendingOptions(sendingOptions);
        }
    }
}
