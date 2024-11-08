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
    internal class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.Property(o => o.Name).HasMaxLength(100).IsRequired();
            builder.Property(o => o.JobTitle).HasMaxLength(255).IsRequired();
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Address).HasMaxLength(100).IsRequired();
        }
    }
}
