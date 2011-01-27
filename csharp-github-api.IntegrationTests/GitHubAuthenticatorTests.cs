using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using csharp_github_api.Framework;
using StructureMap;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class GitHubAuthenticatorTests
    {
        private IAuthenticator _authenticator;
        private RestRequest _restRequest;

        [TestFixtureSetUp]
        public void Setup()
        {
            Bootstrap.Bootstrapper.Bootstrap();
            _authenticator = ObjectFactory.GetInstance<GitHubAuthenticator>();

            _restRequest = new RestRequest
            {
                Resource = "/user/show/sgrassie"
            };
        }

        [Test]
        public void MakeAuthenticatedRequest()
        {
            var client = new RestClient
                             {
                                 BaseUrl = "http://github.com/api/v2/json",
                                 Authenticator = _authenticator
                             };

            var response = client.Execute(_restRequest);

            Assert.That(response.Content, Is.StringContaining("total_private_repo_count"));
        }

        [Test]
        public void MakeAuthenticatedRequestWithToken()
        {
            var client = new RestClient
                             {
                                 BaseUrl = "http://github.com/api/v2/json",
                                 Authenticator = _authenticator
                             };

            var response = client.Execute(_restRequest);

            Assert.That(response.Content, Is.StringContaining("total_private_repo_count"));
        }
    }
}
