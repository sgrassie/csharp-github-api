using NUnit.Framework;
using RestSharp;

namespace csharp_github_api.IntegrationTests.Authentication
{
    [TestFixture]
    public class BasicAuthenticationTest
    {
        private SecretsHandler _secretsHandler;
        private RestRequest _restRequest;

        [SetUp]
        public void Setup()
        {
            _secretsHandler = new SecretsHandler(@"C:\development\secretpasswordfile.xml");

            _restRequest = new RestRequest
            {
                Resource = "/users/sgrassie"
            };
        }

        [Test]
        public void MakeAuthenticatedRequest()
        {
            var client = new RestClient
                             {
                                 BaseUrl ="https://api.github.com",
                                 Authenticator = new HttpBasicAuthenticator(_secretsHandler.Username, _secretsHandler.Password)
                             };

            var response = client.Execute(_restRequest);

            Assert.That(response.Content, Is.StringContaining("total_private_repos"));
        }
    }
}
