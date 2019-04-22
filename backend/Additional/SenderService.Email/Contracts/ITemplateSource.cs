﻿using Lingva.DAL.Entities;
using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface ITemplateSource
    {
        Task<EmailTemplate> GetTemplateAsync(int id);
    }
}
