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
        public HttpClient HttpClient { get; set; } = default!;
        public SnackRackDbContext SnackDbContext { get; set; } = default!;

        private DbConnection _dbConnection = default!;
        private readonly IDockerNetwork _network;
        private readonly CancellationTokenSource _cts = new(TimeSpan.FromMinutes(1));
        public TestWebAppFactory()
        {
            _network = new TestcontainersNetworkBuilder()
           .WithName(Guid.NewGuid().ToString("D"))
           .Build();

            _dbContainer
        = new TestcontainersBuilder<MySqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration
            {
                Database = "Snacks",
                Password = "Password1"
            })
            .WithImage("mysql:8.0")
            .WithEnvironment("MYSQL_ROOT_PASSWORD", "Password1")
            .Build();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                string connectionString = _dbContainer.ConnectionString;

                //services.RemoveAll<SnackRackDbContext>();
                //services.RemoveAll<DbContextOptions<SnackRackDbContext>>();
                services.AddMediator(options: options => options.ServiceLifetime = ServiceLifetime.Scoped);
                services.AddDbContext<SnackRackDbContext>(options =>
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _network.CreateAsync(_cts.Token)
            .ConfigureAwait(false);

            await _dbContainer.StartAsync();

            HttpClient = CreateClient();

            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SnackRackDbContext>();
            SnackDbContext = context;
            await context.Database.MigrateAsync();

            _dbConnection = new SqlConnection(_dbContainer.ConnectionString);
            await _dbConnection.OpenAsync();
        }

        public async new Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}

