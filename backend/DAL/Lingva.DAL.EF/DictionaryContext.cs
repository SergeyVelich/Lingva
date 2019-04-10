using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.EF.Context
{
    [ExcludeFromCodeCoverage]
    public class DictionaryContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
