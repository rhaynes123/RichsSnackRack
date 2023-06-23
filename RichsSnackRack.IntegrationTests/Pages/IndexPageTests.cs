using System;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RichsSnackRack.IntegrationTests.Pages
{
	public class IndexPageTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private HttpClient Client { get; set; }
        public IndexPageTests(WebApplicationFactory<Program> factory)
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

