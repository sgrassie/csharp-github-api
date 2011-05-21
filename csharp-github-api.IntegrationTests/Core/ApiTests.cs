using csharp_github_api.Core;
using NUnit.Framework;

namespace csharp_github_api.IntegrationTests.Core
{
    [TestFixture]
    public class ApiTests
    {
        [Test]
        [ExpectedException(typeof(GitHubResponseException))]
        [Ignore("Fix me")]
        public void ExceptionThrownForBadRequest()
        {
            var github = new Github("http://github.com/api/v2/json");
            //github.User.GetFollowers("sgrassie");
        }
    }
}
