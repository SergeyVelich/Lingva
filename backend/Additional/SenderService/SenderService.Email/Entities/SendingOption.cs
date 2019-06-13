using SenderService.Email.EF.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SenderService.Email.Entities
{
    [ExcludeFromCodeCoverage]
    public class SendingOption
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public SendingOption()
        {

        }

        public SendingOption(EmailSendingOption emailSendingOption)
        {
            Host = emailSendingOption.Host;
            Port = emailSendingOption.Port;
            UseSsl = emailSendingOption.UseSsl;
            UserName = emailSendingOption.UserName;
            Password = emailSendingOption.Password;
        }
    }
}
