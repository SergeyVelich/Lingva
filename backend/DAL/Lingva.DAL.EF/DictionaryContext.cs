using Lingva.DAL.EF.Options;
using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.EF.Context
{
    [ExcludeFromCodeCoverage]
    public class DictionaryContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailSendingOption> EmailSendingOptions { get; set; }

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GroupOptions());
            modelBuilder.ApplyConfiguration(new UserOptions());
            modelBuilder.ApplyConfiguration(new GroupUserOptions());
            modelBuilder.ApplyConfiguration(new LanguageOptions());

            modelBuilder.Entity<EmailTemplate>().Ignore(c => c.Parameters);

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public virtual void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(
                new { Id = 1, Name = "en", CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                new { Id = 2, Name = "ru", CreateDate = DateTime.Now, ModifyDate = DateTime.Now });

            modelBuilder.Entity<Group>().HasData(
                new { Id = 1, Name = "Harry Potter", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, Description = "Good movie", Picture = "1" },
                new { Id = 2, Name = "Librium", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, Description = "Eq", Picture = "2" },
                new { Id = 3, Name = "2Guns", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 2, Description = "stuff", Picture = "3" });

            modelBuilder.Entity<User>().HasData(
                new { Id = 1, Name = "Serhii", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Email = "veloceraptor89@gmail.com", Description = "Good movie", Picture = "1" },
                new { Id = 2, Name = "Old", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Email = "tucker_serega@mail.ru", Description = "Eq", Picture = "2" });

            modelBuilder.Entity<GroupUser>().HasData(
                new { Id = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, GroupId = 1, UserId = 1 },
                new { Id = 2, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, GroupId = 1, UserId = 2 });

            modelBuilder.Entity<EmailTemplate>().HasData(
                new { Id = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Text = "You will have meeting {{GroupName}} at {{GroupDate}}", ParametersString = "GroupName; GroupDate" });

            modelBuilder.Entity<EmailSendingOption>().HasData(
                new { Id = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Host = "smtp.gmail.com", Port = 587, UseSsl = false, UserName = "worksoftserve@gmail.com", Password = "worksoftserve_90" });
        }
    }
}
