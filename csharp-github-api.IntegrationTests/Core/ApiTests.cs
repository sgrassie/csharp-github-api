using csharp_github_api.Core;
using NUnit.Framework;
using StructureMap;

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
            var github = ObjectFactory.GetInstance<Github>();
            //github.User.GetFollowers("sgrassie");
        }
    }
}
