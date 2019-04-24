using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lingva.DAL.EF.Options
{
    public class GroupOptions : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            //builder
            //    .HasMany(x => x.GroupUsers)
            //    .WithOne(x => x.Group)
            //    .HasForeignKey(gu => gu.GroupId);
        }
    }
}
