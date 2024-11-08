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
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Type).HasMaxLength(100).IsRequired();

            builder.HasOne(p => p.Owner).WithMany(o => o.Projects)
                .HasForeignKey(p => p.OwnerId);
        }
    }
}
