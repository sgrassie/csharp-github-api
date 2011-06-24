using NUnit.Framework;
using RestSharp;
using csharp_github_api.Framework;
using StructureMap;
using csharp_github_api.IntegrationTests.Bootstrap;

namespace csharp_github_api.IntegrationTests.Authentication
{
    [TestFixture]
    public class BasicAuthenticationTest
    {
        private RestRequest _restRequest;
        private IGitHubApiSettings _settings;

        [SetUp]
        public void Setup()
        {
            Bootstrapper.Bootstrap();
            _settings = ObjectFactory.GetInstance<GitHubApiSettings>();
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
                                 Authenticator = new HttpBasicAuthenticator(_settings.Username, _settings.Password)
                             };

            var response = client.Execute(_restRequest);

            Assert.That(response.Content, Is.StringContaining("total_private_repos"));
        }
    }
}
