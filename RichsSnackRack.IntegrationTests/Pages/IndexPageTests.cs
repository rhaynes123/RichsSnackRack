using System.Net.Http.Json;
using RichsSnackRack.Orders;

namespace RichsSnackRack.IntegrationTests.Pages
{
	public class IndexPageTests: IClassFixture<TestWebAppFactory> 
    {
        private readonly HttpClient _client;
        public IndexPageTests(TestWebAppFactory factory)
		{
            _client = factory.HttpClient;
        }

		[Fact]
		public async Task IndexReturnsOk()
		{
            //Arrange & Act
            var response = await _client.GetAsync("/Index");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType!.ToString());
        }
		
    }
}

