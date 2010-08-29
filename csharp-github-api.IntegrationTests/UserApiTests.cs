using NUnit.Framework;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class UserApiTests : IntegrationTestBase
    {
        [Test]
        [Ignore("Ignoring live request.")]
        public void GetUser_shouldReturnDeserialisedUserObject()
        {
            var github = new Github("http://github.com/api/v2/json", @"C:\development\secretpasswordfile.xml");

            var user = github.User.GetUser("sgrassie");

            Assert.That(user.Name, Is.StringMatching("Stuart Grassie"));
        }

        [Test]
        public void GetFollowing_fromUser_shouldReturnSomeData()
        {
            var github = new Github(BaseUrl, "sgrassie", "notmyrealpassworddontbesilly", false);

            var user = github.User.GetUser("sgrassie");

            var following = github.User.GetFollowing(user);

            Assert.That(following, Is.Not.Empty);
        }

        [Test]
        public void GetUser_returns_validUserObject()
        {
            var github = new Github(BaseUrl, "sgrassie", "notmyrealpassworddontbesilly", false);

            var user = github.User.GetUser("defunkt");

            Assert.That(user.Name, Is.StringMatching("Kristopher Walken Wanstrath"));
        }
    }
}
