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

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GroupOptions());
            modelBuilder.ApplyConfiguration(new UserOptions());
            modelBuilder.ApplyConfiguration(new GroupUserOptions());
            modelBuilder.ApplyConfiguration(new LanguageOptions());

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public virtual void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(
                new { Id = 1, Name = "en", CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                new { Id = 2, Name = "ru", CreateDate = DateTime.Now, ModifyDate = DateTime.Now });
        }
    }
}
