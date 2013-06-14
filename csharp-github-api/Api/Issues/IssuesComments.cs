//-----------------------------------------------------------------------
// <copyright file="IssuesComments.cs" company="TemporalCohesion.co.uk">
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
using System.Dynamic;
namespace GitHubAPI.Api.Issues
{
    public static class IssuesComments
    {

        private static readonly ILog Log = LoggingExtensions.Logging.Log.GetLoggerFor(typeof(Issues).FullName);



        //http://developer.github.com/v3/issues/comments/#create-a-comment
        //TODO: do we like this way of specifying URL parameters?  Should we require a Repo Object instead?
        public static IRestResponse<TComment> CreateComment<TComment>(this GithubRestApiClient client,
          string RepoOwner, string Repo, long Number, string CommentText) where TComment : new()
        {
            dynamic data = GetUpdateData(CommentText);

            var request = client.RequestFactory.CreateRequest(
                () =>
                {
                    var req = new RestRequest("/repos/{owner}/{repo}/issues/{number}/comments")
                    {
                        Method = Method.POST,
                        RequestFormat = DataFormat.Json
                    };

                    req.AddBody(data);
                    req.AddUrlSegment("owner", RepoOwner);
                    req.AddUrlSegment("repo", Repo);
                    req.AddUrlSegment("number", Number.ToString());
                    return req;
                });

            var response = client.Execute<TComment>(request);

            return response;
        }



        //TODO: rename? we use this for creates and updates...
        private static dynamic GetUpdateData(string body)
        {
            dynamic data = new ExpandoObject();
            //TODO: is this necessary, it's required, should we throw client side instead if it's empty?  also can we use ?? operator?

            if (!string.IsNullOrEmpty(body)) data.body = body;


            return data;
        }
    }
}
