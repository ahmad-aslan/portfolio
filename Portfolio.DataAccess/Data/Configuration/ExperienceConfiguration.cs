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
    internal class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.JobTitle).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Company).HasMaxLength(100).IsRequired();
            builder.Property(e => e.StartJob).IsRequired();
            builder.Property(e => e.EndJob).IsRequired();

            builder.HasOne(e => e.Owner).WithMany(o => o.Experiences)
                .HasForeignKey(e => e.OwnerId);
        }
    }
}
