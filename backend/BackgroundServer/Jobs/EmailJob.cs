﻿using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using Quartz;
using SenderService.Email.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.Background.Jobs
{
    public class EmailJob : IJob
    {
        private readonly IEmailSender _emailSender;
        private readonly ITemplateSource _templateSource;
        private readonly ISendingOptionsSource _sendingOptionsSource;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        public EmailJob(IEmailSender emailSender, ITemplateSource templateSource, ISendingOptionsSource sendingOptionsSource, IGroupService groupService, IUserService userService)
        {
            _emailSender = emailSender;
            _templateSource = templateSource;
            _sendingOptionsSource = sendingOptionsSource;
            _groupService = groupService;
            _userService = userService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            int id = 1;
            //get info about message rule by id (from IMessageService)
            string subject = "Remember!";
            //string templateDirectoryPath = "wwwroot / EmailTemplate";
            //string templateNameFile = "Book_Sold.html";
            //string[] parameters = { };
            //string host = "smtp.gmail.com";
            //int port = 587;
            //bool useSsl = false;
            //string userName = "worksoftserve@gmail.com";
            //string password = "worksoftserve_90";

            EmailTemplate emailTemplate = await _emailSender.GetTemplateAsync(_templateSource, id);
            await _emailSender.SetSendingOptionsAsync(_sendingOptionsSource, id);

            string body = emailTemplate.Text;
            string[] parameters = emailTemplate.Parameters;
            //string body = "You will have meeting {{GroupName}} at {{GroupDate}}";
            //_emailSender.SetSendingOptions(new SendingOptions()
            //{
            //    Port = port,
            //    Host = host,
            //    UseSsl = useSsl,
            //    UserName = userName,
            //    Password = password,
            //});

            var groupsDto = await _groupService.GetListAsync();
            foreach (var groupDto in groupsDto)
            {
                foreach (string paramName in parameters)
                {
                    body = body.Replace("{{" + paramName + "}}", "dsfdfgdgf");
                }

                List<string> recepients = new List<string>();
                var users = await _userService.GetListByGroupAsync(groupDto.Id);
                foreach (UserDto recepient in users)
                {
                    recepients.Add(recepient.Email);
                }

                //recepients.Add("veloceraptor89@gmail.com");

                if (recepients.Count > 0)
                {
                    await _emailSender.CreateSendAsync(subject, body, recepients);
                }
            }
        }
    }
}
