using Lingva.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<Account>
    {
        public DbSet<Group> Groups { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
                base.OnModelCreating(builder);
        }
    }
}
