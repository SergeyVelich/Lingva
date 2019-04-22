using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueryBuilder.Enums;
using QueryBuilder.QueryOptions;
using SenderService.Email;
using SenderService.Email.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly ITemplateSource _templateSource;
        private readonly ISendingOptionsSource _sendingOptionsSource;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        

        public HomeController(IEmailSender emailSender, ITemplateSource templateSource, ISendingOptionsSource sendingOptionsSource, IGroupService groupService, IUserService userService)
        {
            _emailSender = emailSender;
            _templateSource = templateSource;
            _sendingOptionsSource = sendingOptionsSource;
            _groupService = groupService;
            _userService = userService;
        }

        // GET: api/info/letter
        [HttpGet("letter")]
        public async Task GetLetter()
        {
            int id = 1;
            string subject = "Remember!";

            EmailTemplate emailTemplate = await _emailSender.GetTemplateAsync(_templateSource, id);
            await _emailSender.SetSendingOptionsAsync(_sendingOptionsSource, id);

            string body = emailTemplate.Text;
            string[] parameters = emailTemplate.Parameters;

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

                if (recepients.Count > 0)
                {
                    await _emailSender.CreateSendAsync(subject, body, recepients);
                }               
            }
        }
    }
}