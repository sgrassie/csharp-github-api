using System.Configuration;
using FluentAssertions;
using LoggingExtensions.log4net;
using NUnit.Framework;
using RestSharp;
using csharp_github_api.IntegrationTests.Ext;
using csharp_github_api.Models;

namespace csharp_github_api.IntegrationTests
{
    public class UsersTest
    {
        public abstract class UsersTestsBase : TestsSpecBase
        {
            protected GithubRestApiClient Github;

            protected string User;

            public override void Context()
            {
                base.Context();
                Github = new GithubRestApiClient(GitHubUrl);
            }
        }

        public class when_retrieving_unauthenticated_user : UsersTestsBase
        {
            public override void Because()
            {
                User = Username;
            }

            [Fact]
            public void then_response_data_with_model_should_contain_expected_user()
            {
                var response = Github.GetUser<User>(Username);

                response.Data.Login.Should().Be(User, "The response should be for the specified user.");
            }

            [Fact]
            public void then_response_dynamic_should_have_login_property_with_expected_user()
            {
                var response = Github.GetUser(Username);

                Assert.That(response.Dynamic().login, Is.StringMatching(User));
            }

            [Fact]
            public void then_response_data_with_model_should_not_contain_private_data()
            {
                var user = Github.GetUser<User>(Username);

                user.Data.DiskUsage.Should().Be(0);
            }
        }

        public class when_retrieving_the_authenticated_user : UsersTestsBase
        {
            public override void Because()
            {
                User = Username;
            }

            public override void Context()
            {
                base.Context();
                
                Github = Github.WithAuthentication(Authenticator());
            }

            [Fact]
            public void then_response_data_with_model_should_contain_expected_user()
            {
                var user = Github.GetUser<User>();

                user.Data.DiskUsage.Should().BeGreaterThan(0);
            }
        }

        //[Test]
        //public void WithAuthenticationCallToMethodRequiringAuthenticationShouldBeSuccessful()
        //{
        //    var authenticator = ObjectFactory.GetInstance<IAuthenticator>();
        //    var api = new UserApi(GitHubUrl).WithAuthentication(authenticator);
        //    var user = api.GetUser("sgrassie");
        //    Assert.That(user.DiskUsage, Is.GreaterThan(0));
        //}

        //[Test]
        //public void Should_Get_List_Of_Following()
        //{
        //    var api = new UserApi(GitHubUrl);
        //    var user = api.GetUser("sgrassie");

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
        //    var user = api.GetUser("sgrassie");

        //    Assert.Throws<GitHubAuthorizationException>(() => api.Follow("mono"));
        //}

        //[Test]
        //public void Should_Follow_And_Unfollow_User()
        //{
        //    var api = new UserApi(GitHubUrl).WithAuthentication(ObjectFactory.GetInstance<IAuthenticator>());
        //    var user = api.GetUser("sgrassie");

        //    api.Follow("mono").Should().BeTrue("should follow the user mono");
        //    api.GetFollowing(user).Should().Contain(x => x.Login == "mono");

        //    api.UnFollow("mono").Should().BeTrue("Should unfollow the user mono");
        //    api.GetFollowing(user).Should().NotContain(x => x.Login == "mono");
        //}
    }
}
