//-----------------------------------------------------------------------
// <copyright file="Issues.cs" company="TemporalCohesion.co.uk">
//     Copyright 2012 - Present Stuart Grassie
//
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
//----------------------------------------------------------------------

using LoggingExtensions.Logging;
using RestSharp;
using System;
using System.Dynamic;
namespace GitHubAPI.Api.Issues
{
    public static class Issues
    {
        private static readonly ILog Log = LoggingExtensions.Logging.Log.GetLoggerFor(typeof(Issues).FullName);

        /// <summary>
        /// List all issues across all the authenticated user’s visible repositories including owned repositories, member repositories, and organization repositories:  http://developer.github.com/v3/issues/#list-issues
        /// </summary>
        /// <param name="client">The <see cref="GithubRestApiClient"/> instance to attach to.</param>
        /// <typeparam name="TIssue">The issue model to serialise the JSON data into.</typeparam>
        /// <returns>A <see cref="IRestResponse{TIssue}"/> for the issues.</returns>
        public static IRestResponse<TIssue> GetIssues<TIssue>(this GithubRestApiClient client) where TIssue : new()
        {
            Log.Info(() => "Making request for the issues.");
            var request = client.RequestFactory.CreateRequest(
                () =>
                {
                    var req = new RestRequest("/issues?filter={filter}")
                    {
                        Method = Method.GET
                    };
                    req.AddUrlSegment("filter", "all");
                    return req;
                }
            );

            var response = client.Execute<TIssue>(request);

            return response;
        }


        //http://developer.github.com/v3/issues/#create-an-issue
        //TODO: do we like this way of specifying URL parameters?  Should we require a Repo Object instead?
        public static IRestResponse<TIssue> CreateIssue<TIssue>(this GithubRestApiClient client,
          string RepoOwner, string Repo, string Title, string Body, string Assignee = null, string[] Labels = null  ) where TIssue : new()
        {
            dynamic data = GetUpdateData(Title, Body, Assignee, null, Labels);

            var request = client.RequestFactory.CreateRequest(
                () =>
                {
                    var req = new RestRequest("/repos/{owner}/{repo}/issues")
                    {
                        Method = Method.POST,
                        RequestFormat = DataFormat.Json
                    };
                    
                    req.AddBody(data);
                    req.AddUrlSegment("owner", RepoOwner);
                    req.AddUrlSegment("repo", Repo);
                    return req;
                });

            var response = client.Execute<TIssue>(request);

            return response;
        }


        //http://developer.github.com/v3/issues/#edit-an-issue
        public static IRestResponse<TIssue> EditIssue<TIssue>(this GithubRestApiClient client,
  string RepoOwner, string Repo, long IssueNumber, string Title = null, string Body = null, string Asignee = null, string State = null, string[] Labels = null) where TIssue : new()
        {
            dynamic data = GetUpdateData(Title, Body, Asignee, State, Labels);

            var request = client.RequestFactory.CreateRequest(
                () =>
                {
                    var req = new RestRequest("/repos/{owner}/{repo}/issues/{number}")
                    {
                        Method = Method.PATCH,
                        RequestFormat = DataFormat.Json
                    };

                    req.AddBody(data);
                    req.AddUrlSegment("owner", RepoOwner);
                    req.AddUrlSegment("repo", Repo);
                    req.AddUrlSegment("number", IssueNumber.ToString());
                    return req;
                });

            var response = client.Execute<TIssue>(request);

            return response;
        }
        
        //TODO: rename? we use this for creates and updates...
        private static dynamic GetUpdateData(string title, string body, string assignee = null, string state = null, string[] labels = null)
        {
            dynamic data = new ExpandoObject();
            //TODO: is this necessary, it's required, should we throw client side instead if it's empty?  also can we use ?? operator?

            if (!string.IsNullOrEmpty(title)) data.title = title;
            if (!string.IsNullOrEmpty(body) ) data.body = body;
            if (!string.IsNullOrEmpty(assignee) ) data.assignee = assignee;
            if (!string.IsNullOrEmpty(state)) data.state = state; //TODO: "closed" or "open" only...
            if (labels != null) data.labels = labels;

            return data;
        }
    }
}
