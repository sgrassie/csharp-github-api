using FluentAssertions;
using LoggingExtensions.log4net;
using NUnit.Framework;
using csharp_github_api.IntegrationTests.Ext;
using csharp_github_api.Models;

namespace csharp_github_api.IntegrationTests
{
    public class UsersTest
    {
        public abstract class UsersTestsBase : TestsSpecBase
        {
            protected Github Github;

            protected string User;

            public override void Context()
            {
                Github = new Github(GitHubUrl);
            }
        }

        public class when_retrieving_unauthenticated_user : UsersTestsBase
        {
            public override void Because()
            {
                User = "sgrassie";
            }

            [Fact]
            public void then_response_data_with_model_should_contain_expected_user()
            {
                var response = Github.User().Get<User>("sgrassie");

                response.Data.Login.Should().Be(User, "The response should be for the specified user.");
            }

            [Fact]
            public void then_response_dynamic_should_have_login_property_with_expected_user()
            {
                var response = Github.User().Get("sgrassie");

                Assert.That(response.Dynamic().login, Is.StringMatching(User));
            }

            [Fact]
            public void then_response_data_with_model_should_not_contain_private_data()
            {
                var userApi = Github.User();
                var user = userApi.Get<User>("sgrassie");

                user.Data.DiskUsage.Should().Be(0);
            }
        }

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
