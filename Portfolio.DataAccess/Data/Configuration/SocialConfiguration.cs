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
    internal class SocialConfiguration : IEntityTypeConfiguration<Social>
    {
        public void Configure(EntityTypeBuilder<Social> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
            builder.Property(s => s.Icon).IsRequired();
            builder.Property(s => s.Link).IsRequired();


            builder.HasOne(s => s.Owner).WithMany(o => o.Socials)
                .HasForeignKey(s => s.OwnerId);
        }
    }
}
