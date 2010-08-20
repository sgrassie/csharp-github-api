using NUnit.Framework;
using RestSharp;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class GitHubAuthenticatorTests
    {
        private SecretsHandler _secretsHandler;

        [SetUp]
        public void Setup()
        {
            _secretsHandler = new SecretsHandler(@"C:\secretpasswordfile.xml");
        }

        [Test]
        public void MakeAuthenticatedRequest()
        {
            var client = new RestClient
                             {
                                 BaseUrl = "http://github.com/api/v2/json",
                                 Authenticator = new GitHubAuthenticator(_secretsHandler.Username, _secretsHandler.Password, false)
                             };

            var request = new RestRequest
                               {
                                   Resource = "/user/show/sgrassie"
                               };

            var response = client.Execute(request);

            Assert.That(response.Content, Is.StringContaining("total_private_repo_count"));
        }
    }
}
