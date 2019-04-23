using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Quartz;
using SenderService.Email.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.Background.Jobs
{
    public class EmailJob : IJob
    {
        private readonly IEmailSender _emailSender;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        public EmailJob(IEmailSender emailSender, IGroupService groupService, IUserService userService)
        {
            _emailSender = emailSender;
            _groupService = groupService;
            _userService = userService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            int id = 1;
            string subject = "Remember!";

            string body = await _emailSender.GetTemplateAsync(id);
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
    }
}
