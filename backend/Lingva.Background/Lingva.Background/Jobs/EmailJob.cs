using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Quartz;
using SenderService.Email.Contracts;
using SenderService.SettingsProvider.Core.Contracts;
using SenderService.SettingsProvider.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.Background
{
    public class EmailJob : IJob
    {
        private readonly IEmailSender _emailSender;
        private readonly IEmailSettingsProvider _emailSettingsProvider;
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        private readonly IGroupManager _groupManager;
        private readonly IUserManager _userManager;

        private static bool IsBusy = false;

        public EmailJob(IEmailSender emailSender, IEmailSettingsProvider emailSettingsProvider, IEmailTemplateProvider emailTemplateProvider, 
            IGroupManager groupManager, IUserManager userManager)
        {
            _emailSender = emailSender;
            _emailSettingsProvider = emailSettingsProvider;
            _emailTemplateProvider = emailTemplateProvider;
            _groupManager = groupManager;
            _userManager = userManager;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;

                int id = 1;
                string subject = "Remember!";

                EmailTemplate template = await _emailTemplateProvider.GetTemplateAsync(id);
                string body = template.Text;
                EmailSettings settings = await _emailSettingsProvider.GetSettingsAsync(id);
                _emailSender.Host = settings.Host;
                _emailSender.Port = settings.Port;
                _emailSender.UseSsl = settings.UseSsl;
                _emailSender.UserName = settings.UserName;
                _emailSender.Password = settings.Password;

                var groupsDto = await _groupManager.GetListAsync();
                foreach (var groupDto in groupsDto)
                {
                    body = body.Replace("{{GroupName}}", groupDto.Name);
                    body = body.Replace("{{GroupDate}}", groupDto.Date.ToString());

                    List<string> recepients = new List<string>();
                    var users = await _userManager.GetListByGroupAsync(groupDto.Id);
                    foreach (UserDto recepient in users)
                    {
                        recepients.Add(recepient.Email);
                    }

                    if (recepients.Count > 0)
                    {
                        await _emailSender.CreateSendAsync(subject, body, recepients);
                    }
                }
            }
            catch (Exception ex)
            {
                //logger
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
