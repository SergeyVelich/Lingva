using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Quartz;
using SenderService.Email.EF.Contracts;
using SenderService.Email.EF.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.Background
{
    public class EmailJob : IJob
    {
        private readonly IEFEmailSender _emailSender;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        private static bool IsBusy = false;

        public EmailJob(IEFEmailSender emailSender, IGroupService groupService, IUserService userService)
        {
            _emailSender = emailSender;
            _groupService = groupService;
            _userService = userService;
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

                EmailTemplate template = await _emailSender.GetTemplateAsync(id);
                string body = template.Text;
                await _emailSender.SetSendingOptionsAsync(id);

                var groupsDto = await _groupService.GetListAsync();
                foreach (var groupDto in groupsDto)
                {
                    body = body.Replace("{{GroupName}}", groupDto.Name);
                    body = body.Replace("{{GroupDate}}", groupDto.Date.ToString());

                    List<string> recepients = new List<string>();
                    var users = await _userService.GetListByGroupAsync(groupDto.Id);
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
