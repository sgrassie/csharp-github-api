#if NUNIT
using NUnit.Framework;
#else
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using TestFixtureSetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using SetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif


using System.Configuration;

using RestSharp;
using StructureMap;

namespace GitHubAPI.IntegrationTests.Authentication
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

            NUnit.Framework.Assert.That(response.Content, NUnit.Framework.Is.StringContaining("total_private_repos"));
        }
    }
}
