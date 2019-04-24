using SenderService.Email.Contracts;
using SenderService.Email.EF.Entities;
using System.IO;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Providers
{
    public class IOSendingOptionsProvider : IIOSendingOptionsProvider
    {
        public async Task<EmailSendingOption> GetSendingOptionsAsync(string directoryPath, string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), directoryPath, fileName);

            EmailSendingOption sendingOption = new EmailSendingOption();

            using (var reader = File.OpenText(path))
            {
                sendingOption.Host = await reader.ReadLineAsync();
                sendingOption.Port = int.Parse(await reader.ReadLineAsync());
                sendingOption.UseSsl = bool.Parse(await reader.ReadLineAsync());
                sendingOption.UserName = await reader.ReadLineAsync();
                sendingOption.Password = await reader.ReadLineAsync();
            }

            return sendingOption;
        }
    }
}
