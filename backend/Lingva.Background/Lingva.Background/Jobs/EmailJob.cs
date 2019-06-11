using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Quartz;
using SenderService.Email.EF.Contracts;
using SenderService.Email.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.Background
{
    public class EmailJob : IJob
    {
        private readonly IEFEmailSender _emailSender;
        private readonly IGroupManager _groupManager;
        private readonly IUserManager _userManager;

        private static bool IsBusy = false;

        public EmailJob(IEFEmailSender emailSender, IGroupManager groupManager, IUserManager userManager)
        {
            _emailSender = emailSender;
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

                Template template = await _emailSender.GetTemplateAsync(id);
                string body = template.Text;
                await _emailSender.SetSendingOptionsAsync(id);

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
