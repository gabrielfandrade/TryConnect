using Microsoft.EntityFrameworkCore;
using TryConnect.Models;

namespace TryConnect.Repository
{
    public class TryConnectContext : DbContext, ITryConnectContext
    {
        public TryConnectContext(DbContextOptions<TryConnectContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"
                    Server=DESKTOP-BM00L4V;
                    Database=tryconnectdb;
                    User=SA;
                    Password=password12!;
                    TrustServerCertificate=True;
                ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(student => student.StudentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}