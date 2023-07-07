namespace RichsSnackRack.IntegrationTests.Pages
{
	public class IndexPageTests: IClassFixture<TestWebAppFactory> 
    {
        private readonly TestWebAppFactory Factory;
        private readonly HttpClient Client;
        public IndexPageTests(TestWebAppFactory factory)
		{
            Factory = factory;
            Client = Factory.HttpClient;
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

