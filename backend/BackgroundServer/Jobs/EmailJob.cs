using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Quartz;
using QueryBuilder.Enums;
using QueryBuilder.QueryOptions;
using SenderService.Email.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.Background.Jobs
{
    public class EmailJob : IJob
    {
        private readonly IEmailTemplateProvider _templateProvider;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;

        public EmailJob(IEmailTemplateProvider templateProvider, IGroupService groupService, IUserService userService, IEmailSender emailSender)
        {
            _templateProvider = templateProvider;
            _groupService = groupService;
            _userService = userService;
            _emailSender = emailSender;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //get info about message rule by id (from IMessageService)
            string subject = "Remember!";
            string templateDirectoryPath = "wwwroot / EmailTemplate";
            string templateNameFile = "Book_Sold.html";
            string[] parameters = { };

            string body = await _templateProvider.GetTemplateFromIOAsync(templateDirectoryPath, templateNameFile);

            var groupsDto = await _groupService.GetListAsync();
            foreach(GroupDto groupDto in groupsDto)
            {
                foreach(string paramName in parameters)
                {
                    body = body.Replace("{{" + paramName + "}}", "dsfdfgdgf");
                }

                List<QueryFilter> filters = new List<QueryFilter>();
                filters.Add(new QueryFilter("GroupId", groupDto.Id, FilterOperation.Contains));
                QueryOptions queryOptions = new QueryOptions(filters, null, null, null);
                var users = await _userService.GetListAsync(queryOptions);
                List<string> recepients = new List<string>();
                foreach (UserDto recepient in users)
                {
                    recepients.Add(recepient.Email);
                }
                await _emailSender.CreateSendAsync(subject, body, recepients);
            }            
        }
    }
}
