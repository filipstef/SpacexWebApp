using SpacexWebApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace SpacexWebApp.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public string DbPath { get; }

        public ApplicationDbContext()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory; ;
            var projectDir = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;
            var dataDir = Path.Combine(projectDir, "Data");
            DbPath = System.IO.Path.Join(dataDir, "spacex.db");
        }
        
        // Create a Sqlite database in the Data folder
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
