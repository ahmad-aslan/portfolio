using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Social> Socials { get; set; }
       




        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {            
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
