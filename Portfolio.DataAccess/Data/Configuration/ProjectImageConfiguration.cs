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
    internal class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImage>
    {
        public void Configure(EntityTypeBuilder<ProjectImage> builder)
        {
            builder.Property(o => o.ImageUrl).HasMaxLength(255).IsRequired();

            builder.HasOne(pi => pi.Project).WithMany(p => p.ProjectImages)
                .HasForeignKey(pi => pi.ProjectId);
        }
    }
}
