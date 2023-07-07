using System;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RichsSnackRack.IntegrationTests.Pages
{
	public class IndexPageTests: IClassFixture<TestWebAppFactory> 
    {
        private readonly TestWebAppFactory _factory;
        private HttpClient Client { get; set; }
        public IndexPageTests(TestWebAppFactory factory)
		{
            _factory = factory;
            Client = _factory.CreateClient();
        }

		[Fact]
		public async Task IndexReturnsOk()
		{
            //Arrange & Act
            var response = await Client.GetAsync("/Index");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType!.ToString());
        }
    }
}

