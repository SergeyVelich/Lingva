using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Lingva.DAL.EF.EntityConfigurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            //builder
            //    .HasMany(x => x.GroupUsers)
            //    .WithOne(x => x.Group)
            //    .HasForeignKey(gu => gu.GroupId);
            builder
                .Property(e => e.Date)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}
