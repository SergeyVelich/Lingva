using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lingva.Model
{
    public class DBContext : DbContext
    {
        public DbSet<VocabularyRecord> Vocabulary { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }       
    }
}
