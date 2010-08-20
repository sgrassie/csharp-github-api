using NUnit.Framework;
using RestSharp;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class GitHubAuthenticatorTests
    {
        [Test]
        public void MakeAuthenticatedRequest()
        {
            var client = new RestClient
                             {
                                 BaseUrl = "http://github.com/api/v2/json",
                                 Authenticator = new GitHubAuthenticator("", "", false)
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
