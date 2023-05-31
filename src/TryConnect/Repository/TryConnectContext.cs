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
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> Comments { get; set; }

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
            modelBuilder.Entity<Student>().Ignore(student => student.Posts);
            modelBuilder.Entity<Student>().Ignore(student => student.Comments);
            modelBuilder.Entity<Post>().Ignore(post => post.Student);
            modelBuilder.Entity<Post>().Ignore(post => post.Comments);
            modelBuilder.Entity<PostComment>().Ignore(postComment => postComment.Student);
            modelBuilder.Entity<PostComment>().Ignore(postComment => postComment.Post);

            base.OnModelCreating(modelBuilder);
        }
    }
}