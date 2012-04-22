using FluentAssertions;
using NUnit.Framework;
using csharp_github_api.IntegrationTests.Bootstrap;
using RestSharp;
using StructureMap;
using csharp_github_api.Core;
using csharp_github_api.Extensions;

namespace csharp_github_api.IntegrationTests.Core
{
    [TestFixture]
    public class UserApiTests
    {
        private Github _github;
        private const string GitHubUrl = @"https://api.github.com";

        [TestFixtureSetUp]
        public void Setup()
        {
            Bootstrapper.Bootstrap();
            _github = ObjectFactory.GetInstance<Github>();
        }

        [Test]
        public void GetUser_shouldReturnDeserialisedUserObject()
        {
            var user = _github.User.GetUser("sgrassie");

            Assert.That(user.Name, Is.StringMatching("Stuart Grassie"));
        }

        [Test]
        public void GetUser_returns_validUserObject()
        {
            var user = _github.User.GetUser("defunkt");

            Assert.That(user.Name, Is.StringMatching("Chris Wanstrath"));
        }

        [Test]
        public void UnAuthenticatedCallShouldFail()
        {
            var api = new UserApi(GitHubUrl);
            var user = api.GetUser("sgrassie");
            
            Assert.That(user.DiskUsage, Is.EqualTo(0));
        }

        [Test]
        public void WithAuthenticationCallToMethodRequiringAuthenticationShouldBeSuccessful()
        {
            var authenticator = ObjectFactory.GetInstance<IAuthenticator>();
            var api = new UserApi(GitHubUrl).WithAuthentication(authenticator);
            var user = api.GetUser("sgrassie");
            Assert.That(user.DiskUsage, Is.GreaterThan(0));
        }

        [Test]
        public void Should_Get_List_Of_Following()
        {
            var api = new UserApi(GitHubUrl);
            var user = api.GetUser("sgrassie");

            api.GetFollowing(user).Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Should_Get_List_Of_Followers()
        {
            var result = new UserApi(GitHubUrl).GetFollowers("sgrassie");

            Assert.That(result, Has.Count.GreaterThan(0));
        }

        [Test]
        public void WhenNotAuthenticatedFollowShouldThrowNotAuthenticatedException()
        {
            var api = new UserApi(GitHubUrl);
            var user = api.GetUser("sgrassie");

            Assert.Throws<GitHubAuthorizationException>(() => api.Follow("mono"));
        }

        [Test]
        public void Should_Follow_And_Unfollow_User()
        {
            var api = new UserApi(GitHubUrl).WithAuthentication(ObjectFactory.GetInstance<IAuthenticator>());
            var user = api.GetUser("sgrassie");

            api.Follow("mono").Should().BeTrue("should follow the user mono");
            api.GetFollowing(user).Should().Contain(x => x.Login == "mono");

            api.UnFollow("mono").Should().BeTrue("Should unfollow the user mono");
            api.GetFollowing(user).Should().NotContain(x => x.Login == "mono");
        }
    }
}
