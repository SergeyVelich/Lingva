using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenderService.Email.EF;
using SenderService.Email.EF.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly EFEmailSender _emailSender;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        

        public HomeController(EFEmailSender emailSender, IGroupService groupService, IUserService userService)
        {
            _emailSender = emailSender;
            _groupService = groupService;
            _userService = userService;
        }

        // GET: api/info/letter
        [HttpGet("letter")]
        public async Task GetLetter()
        {
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
    }
}