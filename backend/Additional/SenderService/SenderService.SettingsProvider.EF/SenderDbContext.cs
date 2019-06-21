using Microsoft.EntityFrameworkCore;
using SenderService.SettingsProvider.Core.Entities;
using SenderService.SettingsProvider.EF.EntityConfigurations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SenderService.SettingsProvider.EF.Context
{
    [ExcludeFromCodeCoverage]
    public class SenderDbContext : DbContext
    {
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailSettings> EmailSettings { get; set; }

        public SenderDbContext(DbContextOptions<SenderDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new EmailSendingOptionConfiguration());
        
            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public virtual void Seed(ModelBuilder modelBuilder)
        {          
            modelBuilder.Entity<EmailTemplate>().HasData(
                new { Id = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Text = "You will have meeting {{GroupName}} at {{GroupDate}}", ParametersString = "GroupName; GroupDate" });

            modelBuilder.Entity<EmailSettings>().HasData(
                new { Id = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Host = "smtp.gmail.com", Port = 587, UseSsl = false, UserName = "worksoftserve@gmail.com", Password = "worksoftserve_90" });
        }

        public async Task InitializeAsync()
        {
            await Database.MigrateAsync();
        }
    }
}
