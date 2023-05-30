using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TryConnect.Repository;

namespace TryConnect.Test;

public class TryConnectTestContext<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<TryConnectContext>));
            if (descriptor != null)
                services.Remove(descriptor);
            services.AddDbContext<TryConnectContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTryContextTest");
                });
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<TryConnectContext>())
            {
                try
                {
                    appContext.Database.EnsureCreated();
                    appContext.Students.AddRange(
                        Helpers.GetStudentsListForTests()
                    );
                    appContext.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }    
        });
    }
}