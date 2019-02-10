using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lingva.Model
{
    public class DBContext : DbContext
    {
        public DbSet<DictionaryRecord> Dictionary { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DictionaryRecord>()
                   .HasOne(m => m.TranslationLanguage)
                   .WithMany(t => t.TranslationDictionary)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSetItem>()
                   .HasOne(m => m.UserSet)
                   .WithMany(t => t.UserSetItems)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
