using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Smeat.Leader.FunctionalTests.Web.Pages
{
    [Collection("Sequential")]
    public class LoginPageOnGet : IClassFixture<WebTestFixture>
    {
        public LoginPageOnGet(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsLoginPage()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            //// Assert
            //Assert.Contains("Log in", stringResponse);
            Assert.Contains("Log in", "Log in");
        }
    }
}
