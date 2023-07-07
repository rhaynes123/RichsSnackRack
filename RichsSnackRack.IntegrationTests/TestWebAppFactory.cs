using System;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RichsSnackRack.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using DotNet.Testcontainers.Networks;

namespace RichsSnackRack.IntegrationTests
{
    public class TestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly TestcontainerDatabase _dbContainer;
        private readonly IDockerNetwork _network;
        private readonly CancellationTokenSource _cancellationTokenSource = new(TimeSpan.FromMinutes(1));

        public HttpClient HttpClient { get; private set; } = default!;
        public SnackRackDbContext SnackDbContext { get; private set; } = default!;
        
        public TestWebAppFactory()
        {
            _network = new TestcontainersNetworkBuilder()
           .WithName(Guid.NewGuid().ToString("D"))
           .Build();

            _dbContainer = new TestcontainersBuilder<MySqlTestcontainer>()
            .WithDatabase(new MySqlTestcontainerConfiguration
            {
                Database = "SnackRack",
                Password = "Password1",
                Username = "root"
            })
            
            .WithImage("mysql:8.0")
            .WithNetwork(_network)
            .WithNetworkAliases("db")
            .Build();

        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            string connectionString = _dbContainer.ConnectionString;
            builder.ConfigureTestServices(services =>
            {

                services.RemoveAll<SnackRackDbContext>();
                services.RemoveAll<DbContextOptions<SnackRackDbContext>>();
                services.AddDbContext<SnackRackDbContext>(options =>
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });

                var provider = services.BuildServiceProvider();
                SnackDbContext = provider.GetRequiredService<SnackRackDbContext>();

            });
            
        }

        public async Task InitializeAsync()
        {
            await _network.CreateAsync(_cancellationTokenSource.Token);

            await _dbContainer.StartAsync(_cancellationTokenSource.Token);

            HttpClient = CreateClient();

            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SnackRackDbContext>();
            await context.Database.MigrateAsync();
        }

        public async new Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}

