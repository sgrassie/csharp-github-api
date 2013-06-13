using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using GitHubAPI.Models;

using GitHubAPI.Api.Users;
using GitHubAPI.Api.Issues;

using RestSharp;
using System.Collections.Generic;

namespace GitHubAPI.IntegrationTests
{
    [TestClass]
    public class IssueTests
    {
        protected GithubRestApiClient client;

        protected const string GitHubUrl = @"https://api.github.com";

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Token { get; private set; }

        public IssueTests()
        {
            
            Username = ConfigurationManager.AppSettings["username"];
            Password = ConfigurationManager.AppSettings["password"];
            Token = ConfigurationManager.AppSettings["token"];

            client = new GithubRestApiClient(GitHubUrl);
        }

        /// <summary>
        /// By default returns a <see cref="HttpBasicAuthenticator"/>. Override to return a different implementation.
        /// </summary>
        /// <returns>A <see cref="HttpBasicAuthenticator"/></returns>
        public virtual IAuthenticator Authenticator()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password)
                       ? new HttpBasicAuthenticator(Username, Password)
                       : null;
        }



        [TestMethod]
        public void GetIssuesTest()
        {

            var response = client.WithAuthentication(Authenticator()).GetIssues<List<Issue>>();

        }




        [TestMethod]
        public void CreateIssueTest()
        {


            var response = client.WithAuthentication(Authenticator()).CreateIssue<Issue>(Username, "APITest", "Test Title", "Body of text", Username, new string[] { "dump" });


        }


        [TestMethod]
        public void CreateUpdateIssueTest()
        {
            var c = client.WithAuthentication(Authenticator());
            
            var response = c.CreateIssue<Issue>(Username, "APITest", "Test Title", "Body of text", Username, new string[] { "dump" });
            response = c.EditIssue<Issue>(Username, "APITest", response.Data.Number, State: "closed");

        }
    }
}
