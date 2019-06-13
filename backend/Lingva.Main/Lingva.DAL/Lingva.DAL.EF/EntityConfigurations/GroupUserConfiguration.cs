using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lingva.DAL.EF.EntityConfigurations
{
    public class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder
                .HasKey(gu => new { gu.GroupId, gu.UserId });
            builder
                .HasOne(gu => gu.Group)
                .WithMany(b => b.GroupUsers)
                .HasForeignKey(gu => gu.GroupId);
            builder
                .HasOne(gu => gu.User)
                .WithMany(c => c.GroupUsers)
                .HasForeignKey(gu => gu.UserId);
        }
    }
}
