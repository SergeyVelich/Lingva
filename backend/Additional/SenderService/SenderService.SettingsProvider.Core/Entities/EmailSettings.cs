using System.Diagnostics.CodeAnalysis;

namespace SenderService.SettingsProvider.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class EmailSettings
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
