using Microsoft.EntityFrameworkCore;
using TryConnect.Models;
using TryConnect.Repository;

namespace TryConnect.Test;

public class TryConnectTestContext : DbContext, ITryConnectContext
{
    public TryConnectTestContext(DbContextOptions<TryConnectContext> options)
            : base(options)
    { }

    public DbSet<Student> Students { get; set; }
}