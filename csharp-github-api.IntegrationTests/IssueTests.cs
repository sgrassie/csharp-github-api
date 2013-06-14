#if NUNIT
using NUnit.Framework;
#else
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Fact = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using TestFixtureSetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using SetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif


using System;
using System.Configuration;
using GitHubAPI.Models;

using GitHubAPI.Api.Issues;
using FluentAssertions;
using RestSharp;
using System.Collections.Generic;

namespace GitHubAPI.IntegrationTests
{
    [TestFixture]
    public class IssueTests
    {

        public abstract class GetIssuesTestsBase : GitHubAPI.IntegrationTests.Ext.TestsSpecBase
        {
            protected GithubRestApiClient Github;

            protected string User;

            protected IRestResponse<List<Issue>> Response = null;

            public override void Context()
            {
                base.Context();
                Github = new GithubRestApiClient(GitHubUrl);
            }
        }


        [TestFixture]
        public class when_retrieving_issues_unauthenticated : GetIssuesTestsBase
        {
            public override void Because()
            {
                User = Username;
                Response = Github.GetIssues<List<Issue>>();
            }

            [Fact]
            public void then_response_data_with_model_should_have_one()
            {
                //TODO: it returns {message : "Not Found"}, what should we do about that?s
                Response.Data.Should().HaveCount(1); 
            }

            [Fact]
            public void then_response_dynamic_should_have_message_not_found()
            {
                //TODO: why can't i do response.Dynamic().message?
                NUnit.Framework.Assert.That(Response.Dynamic()["message"], NUnit.Framework.Is.StringMatching("Not Found"));
            }

            [Fact]
            public void then_response_data_with_model_should_not_contain_data()
            {
                Response.Data[0].Number.Should().Be(0);
            }
        }


        [TestFixture]
        public class when_retrieving_issues_authenticated : GetIssuesTestsBase
        {
            public override void Because()
            {
                User = Username;
                Github = Github.WithAuthentication(Authenticator());
                Response = Github.GetIssues<List<Issue>>();
            }


            [Fact]
            public void then_response_data_with_model_should_contain_some_issues()
            {
                Response.Data.Should().NotBeEmpty();
            }


            
        }

 

        public abstract class CreateIssueTestsBase : GitHubAPI.IntegrationTests.Ext.TestsSpecBase
        {
            protected GithubRestApiClient Github;

            protected string User;
            protected string RepoOwnerName;
            protected string RepoName;
            protected string[] Labels;
            protected string Title;
            protected string Body;

            protected IRestResponse<Issue> Response;

            public override void Context()
            {
                base.Context();
                Github = new GithubRestApiClient(GitHubUrl);
            }
        }


        [TestFixture]
        public class when_creating_issues_unauthenticated : CreateIssueTestsBase
        {
            public override void Because()
            {
                User = Username;
                RepoName = "Whatever";
                RepoOwnerName = "Whatever";
                Response = Github.CreateIssue<Issue>(RepoOwnerName, RepoName, Title, Body, User, Labels);
            }

            [Fact]
            public void then_response_data_with_model_should_be_empty()
            {
                Response.Data.Assignee.Should().BeNull();
                Response.Data.Number.Should().Be(0);
                Response.Data.Title.Should().BeNull();
                Response.Data.User.Should().BeNull();
                
            }

            [Fact]
            public void then_response_dynamic_should_have_message_not_found()
            {
                //Note this will fail unless the JsonObject in SimpleJson has define dynamic set
                NUnit.Framework.Assert.That(Response.Dynamic().message, NUnit.Framework.Is.StringMatching("Not Found"));
            }

        }


        [TestFixture]
        public class when_creating_issues_authenticated_to_nonexistant_repo : CreateIssueTestsBase
        {
            public override void Because()
            {
                User = Username;
                RepoName = "Whatever";
                RepoOwnerName = "Whatever";

                Github = Github.WithAuthentication(Authenticator());
                Response = Github.CreateIssue<Issue>(RepoOwnerName, RepoName, Title, Body, User, Labels);
            }

            [Fact]
            public void then_response_data_with_model_should_be_empty()
            {
                Response.Data.Assignee.Should().BeNull();
                Response.Data.Number.Should().Be(0);
                Response.Data.Title.Should().BeNull();
                Response.Data.User.Should().BeNull();

            }

            [Fact]
            public void then_response_dynamic_should_have_message_not_found()
            {
                //TODO: why can't i do response.Dynamic().message?
                NUnit.Framework.Assert.That(Response.Dynamic()["message"], NUnit.Framework.Is.StringMatching("Not Found"));
            }

        }

        [TestFixture]
        public class when_creating_issues_authenticated_to_unauthorized_repo : CreateIssueTestsBase
        {
            public override void Because()
            {
                User = Username;
                RepoName = "NeedARepoYouCantCreateIssuesIn";
                RepoOwnerName = "Whatever";

                Github = Github.WithAuthentication(Authenticator());
                Response = Github.CreateIssue<Issue>(RepoOwnerName, RepoName, Title, Body, User, Labels);
            }

            [Fact]
            [Ignore] //TODO: setup a repo that matches requirements
            public void then_response_data_with_model_should_be_empty()
            {
                Response.Data.Assignee.Should().BeNull();
                Response.Data.Number.Should().Be(0);
                Response.Data.Title.Should().BeNull();
                Response.Data.User.Should().BeNull();

            }

            [Fact]
            [Ignore] //TODO: setup a repo that matches requirements
            public void then_response_dynamic_should_have_message_not_found()
            {
                //TODO: why can't i do response.Dynamic().message?
                NUnit.Framework.Assert.That(Response.Dynamic()["message"], NUnit.Framework.Is.StringMatching("Not Found"));
            }

        }


        [TestFixture]
        public class when_creating_issues_authenticated_to_authorized_repo : CreateIssueTestsBase
        {
            public override void Because()
            {
                User = Username;
                RepoName = "NeedARepoYouCanCreateIssuesIn";
                RepoOwnerName = "Whatever";

                Github = Github.WithAuthentication(Authenticator());
                Response = Github.CreateIssue<Issue>(RepoOwnerName, RepoName, Title, Body, User, Labels);
            }

            [Fact]
            [Ignore] //TODO: setup a repo that matches requirements
            public void then_response_data_with_model_should_have_issue_data()
            {
                //TODO: what should we check for

            }

        }

        public abstract class EditIssueTestsBase : GitHubAPI.IntegrationTests.Ext.TestsSpecBase
        {
            protected GithubRestApiClient Github;

            protected string User;
            protected string RepoOwnerName;
            protected string RepoName;
            protected string[] Labels;
            protected string Title;
            protected string Body;

            protected IRestResponse<Issue> Response;

            protected Issue IssueToUpdate;

            public override void Context()
            {
                base.Context();
                Github = new GithubRestApiClient(GitHubUrl);
            }
        }


        [TestFixture]
        public class when_editing_issues_authenticated_to_existent_issue : EditIssueTestsBase
        {
            public override void Because()
            {
                User = Username;
                RepoName = "Whatever";
                RepoOwnerName = "Whatever";

                Github = Github.WithAuthentication(Authenticator());
                IssueToUpdate = Github.CreateIssue<Issue>(RepoOwnerName, RepoName, "Isssue To Update", "Text to Update", User).Data;

                Response = Github.EditIssue<Issue>(RepoOwnerName, RepoName, IssueToUpdate.Number);
            }

            [Fact]
            [Ignore] //TODO: setup repo with right permissions
            public void then_response_data_with_model_should_have_updates()
            {
                //TODO: add changes to EditIssue call and then check here
            }

        }

    }
  
 
}
