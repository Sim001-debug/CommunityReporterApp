using CommunityReporterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityReporterApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}
