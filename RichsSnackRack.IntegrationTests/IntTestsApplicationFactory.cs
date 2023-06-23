using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RichsSnackRack.Persistence;
namespace RichsSnackRack.IntegrationTests
{
	public class IntTestsApplicationFactory : WebApplicationFactory<Program>
    {
		public IntTestsApplicationFactory()
		{
		}
        protected override Microsoft.AspNetCore.Hosting.IWebHostBuilder? CreateWebHostBuilder()
        {
            return base.CreateWebHostBuilder();
        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            builder.ConfigureServices(services =>
            {
                services.AddScoped(scopedServices =>
                {
                    return new DbContextOptionsBuilder<SnackRackDbContext>()
                    .UseInMemoryDatabase("TestingDb")
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .UseApplicationServiceProvider(scopedServices)
                    .Options;
                });
                services.AddMediator(options: option => option.ServiceLifetime = ServiceLifetime.Scoped);
            });
            return base.CreateHost(builder);
        }
    }
}

