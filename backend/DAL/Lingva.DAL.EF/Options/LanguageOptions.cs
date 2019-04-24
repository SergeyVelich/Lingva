using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lingva.DAL.EF.Options
{
    public class LanguageOptions : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder
                .HasMany(x => x.Groups)
                .WithOne(x => x.Language)
                .HasForeignKey(g => g.LanguageId);
        }
    }
}
