using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using csharp_github_api.API;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class UserApiTests : IntegrationTestBase
    {
        [Test]
        [Ignore("Ignoring live request.")]
        public void GetUser_shouldReturnDeserialisedUserObject()
        {
            var github = new Github(@"C:\development\secretpasswordfile.xml")
                             {
                                 BaseUrl = "http://github.com/api/v2/json"
                             };

            var user = github.GetUser("sgrassie");

            Assert.That(user.Name, Is.StringMatching("Stuart Grassie"));
        }

        [Test]
        public void GetFollowing_fromUser_shouldReturnSomeData()
        {
            var github = new Github(@"C:\development\secretpasswordfile.xml")
            {
                BaseUrl = "http://github.com/api/v2/json"
            };

            var user = github.GetUser("sgrassie");

            var following = user.GetFollowing();

            Assert.That(following, Is.Not.Empty);
        }

        [Test]
        public void GetUser_returns_validUserObject()
        {
            var github = new Github(@"C:\development\secretpasswordfile.xml")
            {
                BaseUrl = "http://localhost:8080"
            };

            var user = github.GetUser("defunkt");

            Assert.That(user.Name, Is.StringMatching("Kristopher Walken Wanstrath"));
        }
    }
}
