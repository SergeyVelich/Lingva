using MimeKit;
using SenderService.Email.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenderService.Email
{
    public class EmailMessageBuilder : IEmailMessageBuilder
    {
        private readonly IEmailMessageBodyProvider _bodyProvider;

        public EmailMessageBuilder(IEmailMessageBodyProvider bodyProvider)
        {
            _bodyProvider = bodyProvider;
        }

        public async Task<MimeMessage> CreateAsync(string text, string title, string toEmail, params KeyValuePair<string, string>[] replacers)
        {
            MimeMessage emailMessage = new MimeMessage();

            BodyBuilder bodyBuilder = new BodyBuilder
            {
                HtmlBody = await _bodyProvider.GetMessageBodyAsync(text)
            };

            foreach (KeyValuePair<string, string> replacer in replacers)
            {
                bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("{{" + replacer.Value + "}}", replacer.Value);
            }

            emailMessage.From.Add(new MailboxAddress("From no reply", "noReplyAdministration@gmail.com"));
            emailMessage.Date = DateTime.UtcNow;
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = "Book sold";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = bodyBuilder.HtmlBody
            };

            return emailMessage;
        }         
    }
}
