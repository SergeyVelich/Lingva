using Microsoft.EntityFrameworkCore;
using SenderService.Email.EF.DAL.Entities;
using System;

namespace SenderService.Email.EF.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddEmailSender(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailTemplate>().Ignore(c => c.Parameters);
            modelBuilder.Entity<EmailTemplate>().HasData(
                new { Id = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Text = "You will have meeting {{GroupName}} at {{GroupDate}}", ParametersString = "GroupName; GroupDate" });

            modelBuilder.Entity<EmailSendingOption>().HasData(
                new { Id = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Host = "smtp.gmail.com", Port = 587, UseSsl = false, UserName = "worksoftserve@gmail.com", Password = "worksoftserve_90" });
        }
    }
}
