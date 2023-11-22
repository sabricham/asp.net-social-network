using uvibe_social_network_website.Models;
using Microsoft.EntityFrameworkCore;

namespace uvibe_social_network_website.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
