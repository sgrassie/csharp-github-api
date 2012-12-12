using System.Configuration;
using NUnit.Framework;
using RestSharp;
using StructureMap;

namespace csharp_github_api.IntegrationTests.Authentication
{
    [TestFixture]
    public class BasicAuthenticationTest
    {
        private RestRequest _restRequest;
        private string _username;
        private string _password;

        [SetUp]
        public void Setup()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration("csharp-github-api.IntegrationTests.dll");
            _username = config.AppSettings.Settings["username"].Value;
            _password = config.AppSettings.Settings["password"].Value;

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
                                 Authenticator = new HttpBasicAuthenticator(_username, _password)
                             };

            var response = client.Execute(_restRequest);

            Assert.That(response.Content, Is.StringContaining("total_private_repos"));
        }
    }
}
