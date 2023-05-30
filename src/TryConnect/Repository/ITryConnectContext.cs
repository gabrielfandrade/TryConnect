using Microsoft.EntityFrameworkCore;
using TryConnect.Models;

namespace TryConnect.Repository
{
    public interface ITryConnectContext
    {
        public DbSet<Student> Students { get; set; }
    }
}