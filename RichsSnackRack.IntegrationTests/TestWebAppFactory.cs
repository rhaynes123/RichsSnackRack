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
        private const ushort HttpPort = 80;
        private readonly TestcontainerDatabase _dbContainer;
        private readonly IDockerNetwork _network;

        private readonly IDockerContainer _appContainer;
        private readonly CancellationTokenSource _cancellationTokenSource = new(TimeSpan.FromMinutes(1));

        public HttpClient HttpClient { get; set; } = default!;
        public SnackRackDbContext SnackDbContext { get; set; } = default!;

        private DbConnection _dbConnection = default!;
        
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
            //.WithEnvironment("MYSQL_USER","root")
            //.WithEnvironment("MYSQL_ROOT_PASSWORD", "Password1")
            .WithEnvironment("MYSQL_ALLOW_EMPTY_PASSWORD", "true")
            .WithNetwork(_network)
            .WithNetworkAliases("db")
            .Build();

            _appContainer = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("richssnackrack")
                .WithNetwork(_network)
                .WithPortBinding(HttpPort, true)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(HttpPort))
                .Build();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            string connectionString = _dbContainer.ConnectionString;
            builder.UseEnvironment("CONTAINER");
            builder.ConfigureTestServices(services =>
            {

                services.RemoveAll<SnackRackDbContext>();
                services.RemoveAll<DbContextOptions<SnackRackDbContext>>();
                //services.AddMediator(options: options => options.ServiceLifetime = ServiceLifetime.Scoped);
                services.AddDbContext<SnackRackDbContext>(options =>
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _network.CreateAsync(_cancellationTokenSource.Token);

            await _dbContainer.StartAsync(_cancellationTokenSource.Token);
            string connectionString = _dbContainer.ConnectionString;
            //await _appContainer.StartAsync(_cancellationTokenSource.Token);

            HttpClient = CreateClient();

            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SnackRackDbContext>();
            SnackDbContext = context;
            await context.Database.MigrateAsync();

            //_dbConnection = new SqlConnection(_dbContainer.ConnectionString);
           // await _dbConnection.OpenAsync();
        }

        public async new Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}

