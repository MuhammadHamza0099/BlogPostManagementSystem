using BPMS.API.Data.Entities;
using BPMS.API.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BPMS.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply Fluent Configurations
            modelBuilder.ApplyConfiguration(new BlogPostConfiguration());

        }
    }
}
