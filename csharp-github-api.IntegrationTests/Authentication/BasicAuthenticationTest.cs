namespace GitHubAPI.IntegrationTests.Authentication
{
    using NUnit.Framework;
    using System.Configuration;

    using RestSharp;

    [TestFixture]
    public class BasicAuthenticationTest
    {
        private RestRequest _restRequest;
        private string _username;
        private string _password;

        [SetUp]
        public void Setup()
        {

            _username = ConfigurationManager.AppSettings["username"];
            _password = ConfigurationManager.AppSettings["password"];

            _restRequest = new RestRequest
            {
                Resource = "/users/" + _username
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
