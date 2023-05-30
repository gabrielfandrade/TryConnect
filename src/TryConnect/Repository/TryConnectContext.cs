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
    }
}