using NUnit.Framework;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class UsersTestsUnAuthenticated
    {
        private Github _github;
        private const string GitHubUrl = @"https://api.github.com";

        [TestFixtureSetUp]
        public void Setup()
        {
            _github = new Github(GitHubUrl).WithLogger(type => new DebugLogger(type));
        }

        [Test]
        public void GetUser_shouldReturnDeserialisedUserObject()
        {
            var response = _github.User().Get("sgrassie");

            Assert.That(response.Dynamic().login, Is.StringMatching("sgrassie"));
        }

        //[Test]
        //public void GetUser_returns_validUserObject()
        //{
        //    var user = _github.User.Get("defunkt");

        //    Assert.That(user.Name, Is.StringMatching("Chris Wanstrath"));
        //}

        //[Test]
        //public void UnAuthenticatedCallShouldFail()
        //{
        //    var api = new UserApi(GitHubUrl);
        //    var user = api.Get("sgrassie");
            
        //    Assert.That(user.DiskUsage, Is.EqualTo(0));
        //}

        //[Test]
        //public void WithAuthenticationCallToMethodRequiringAuthenticationShouldBeSuccessful()
        //{
        //    var authenticator = ObjectFactory.GetInstance<IAuthenticator>();
        //    var api = new UserApi(GitHubUrl).WithAuthentication(authenticator);
        //    var user = api.Get("sgrassie");
        //    Assert.That(user.DiskUsage, Is.GreaterThan(0));
        //}

        //[Test]
        //public void Should_Get_List_Of_Following()
        //{
        //    var api = new UserApi(GitHubUrl);
        //    var user = api.Get("sgrassie");

        //    api.GetFollowing(user).Count.Should().BeGreaterThan(0);
        //}

        //[Test]
        //public void Should_Get_List_Of_Followers()
        //{
        //    var result = new UserApi(GitHubUrl).GetFollowers("sgrassie");

        //    Assert.That(result, Has.Count.GreaterThan(0));
        //}

        //[Test]
        //public void WhenNotAuthenticatedFollowShouldThrowNotAuthenticatedException()
        //{
        //    var api = new UserApi(GitHubUrl);
        //    var user = api.Get("sgrassie");

        //    Assert.Throws<GitHubAuthorizationException>(() => api.Follow("mono"));
        //}

        //[Test]
        //public void Should_Follow_And_Unfollow_User()
        //{
        //    var api = new UserApi(GitHubUrl).WithAuthentication(ObjectFactory.GetInstance<IAuthenticator>());
        //    var user = api.Get("sgrassie");

        //    api.Follow("mono").Should().BeTrue("should follow the user mono");
        //    api.GetFollowing(user).Should().Contain(x => x.Login == "mono");

        //    api.UnFollow("mono").Should().BeTrue("Should unfollow the user mono");
        //    api.GetFollowing(user).Should().NotContain(x => x.Login == "mono");
        //}
    }
}
