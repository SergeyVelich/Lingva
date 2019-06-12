using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenderService.SettingsProvider.Core.Entities;

namespace SenderService.SettingsProvider.EF.EntityConfigurations
{
    public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder
                .Ignore(c => c.Parameters);
        }
    }
}
