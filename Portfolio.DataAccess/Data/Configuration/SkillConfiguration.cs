using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Data.Configuration
{
    internal class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SkillName).HasMaxLength(100).IsRequired();
            builder.Property(s => s.Rate).IsRequired();


            builder.HasOne(s => s.Owner).WithMany(o => o.Skills)
                .HasForeignKey(s => s.OwnerId);
        }
    }
}
