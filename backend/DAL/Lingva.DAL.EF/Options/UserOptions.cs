using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lingva.DAL.EF.Options
{
    public class UserOptions : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder
            //    .HasMany(x => x.GroupUsers)
            //    .WithOne(x => x.User)
            //    .HasForeignKey(gu => gu.UserId);
        }
    }
}
