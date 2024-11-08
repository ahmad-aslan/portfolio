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
    internal class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Degree).HasMaxLength(100).IsRequired();
            builder.Property(e => e.StartEducation).IsRequired();
            builder.Property(e => e.EndEducation).IsRequired();

            builder.HasOne(e => e.Owner).WithMany(o => o.Educations)
                .HasForeignKey(e => e.OwnerId);
        }
    }
}
