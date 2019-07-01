using Lingva.DAL.EF.EntityConfigurations;
using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Context
{
    [ExcludeFromCodeCoverage]
    public class DictionaryContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<User> Users { get; set; }

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GroupUserConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
         
            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public virtual void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(
                new { Id = 1, Name = "en", CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                new { Id = 2, Name = "ru", CreateDate = DateTime.Now, ModifyDate = DateTime.Now });

            modelBuilder.Entity<Group>().HasData(
                new { Id = 1, Name = "Harry Potter", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, Description = "Good movie" },
                new { Id = 2, Name = "Librium", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, Description = "Eq" },
                new { Id = 3, Name = "2Guns", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 2, Description = "stuff" });

            modelBuilder.Entity<User>().HasData(
                new { Id = 1, Name = "Serhii", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Email = "veloceraptor89@gmail.com" },
                new { Id = 2, Name = "Old", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Email = "tucker_serega@mail.ru" });

            modelBuilder.Entity<GroupUser>().HasData(
                new { Id = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, GroupId = 1, UserId = 1 },
                new { Id = 2, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, GroupId = 1, UserId = 2 });
        }

        public async Task InitializeAsync()
        {
            await Database.MigrateAsync();
        }
    }
}
