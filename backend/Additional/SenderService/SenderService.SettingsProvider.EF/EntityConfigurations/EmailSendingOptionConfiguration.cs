using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenderService.SettingsProvider.Core.Entities;

namespace SenderService.SettingsProvider.EF.EntityConfigurations
{
    public class EmailSendingOptionConfiguration : IEntityTypeConfiguration<EmailSettings>
    {
        public void Configure(EntityTypeBuilder<EmailSettings> builder)
        {

        }
    }
}
