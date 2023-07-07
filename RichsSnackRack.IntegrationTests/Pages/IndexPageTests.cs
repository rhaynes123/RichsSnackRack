using System;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RichsSnackRack.IntegrationTests.Pages
{
	public class IndexPageTests: IClassFixture<TestWebAppFactory> //IAsyncLifetime, IDisposable
    {
        private readonly TestWebAppFactory _factory;
        private const ushort HttpPort = 80;

        private readonly CancellationTokenSource _cancellationTokenSource = new(TimeSpan.FromMinutes(1));

        private readonly IDockerNetwork _network;

        private readonly IDockerContainer _dbContainer;

        private readonly IDockerContainer _appContainer;
        private HttpClient Client { get; set; }
        public IndexPageTests(TestWebAppFactory factory)
		{
            _factory = factory;
            Client = _factory.CreateClient();

            //_network = new TestcontainersNetworkBuilder()
            //.WithName(Guid.NewGuid().ToString("D"))
            //.Build();

            //_dbContainer = new TestcontainersBuilder<MySqlTestcontainer>()
            //    .WithDatabase(new MsSqlTestcontainerConfiguration
            //    {
            //        Database = "Snacks",
            //        Password = "Password1"
            //    })
            //    .WithImage("mysql:8.0")
            //    .WithEnvironment("MYSQL_ROOT_PASSWORD", "Password1")
            //    //.WithImage("postgres")
            //    .WithNetwork(_network)
            //    .WithNetworkAliases("db")
            //    .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3306))// Without this the image may hang and timeout
            //    //.WithVolumeMount("postgres-data", "/var/lib/postgresql/data")
            //    .Build();

            //_appContainer = new TestcontainersBuilder<TestcontainersContainer>()
            //    .WithImage("richssnackrack")
            //    .WithNetwork(_network)
            //    .WithPortBinding(HttpPort, true)
            //    .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(HttpPort))
            //    .Build();
        }

		[Fact]
		public async Task IndexReturnsOk()
		{
            //Arrange & Act
            //using var httpClient = new HttpClient();
           // httpClient.BaseAddress = new UriBuilder("http", _appContainer.Hostname, _appContainer.GetMappedPublicPort(HttpPort)).Uri;

            var response = await Client.GetAsync("/Index");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType!.ToString());
        }

        //public void Dispose()
        //{
        //    _cancellationTokenSource.Dispose();
        //}

        //public async Task InitializeAsync()
        //{
        //    await _network.CreateAsync(_cancellationTokenSource.Token)
        //   .ConfigureAwait(false);

        //    await _dbContainer.StartAsync(_cancellationTokenSource.Token)
        //        .ConfigureAwait(false);

        //    await _appContainer.StartAsync(_cancellationTokenSource.Token)
        //        .ConfigureAwait(false);
        //}

        //public Task DisposeAsync()
        //{
        //    return Task.CompletedTask;
        //}
    }
}

