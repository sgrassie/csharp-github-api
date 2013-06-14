//-----------------------------------------------------------------------
// <copyright file="Users.cs" company="TemporalCohesion.co.uk">
//     Copyright 2010 - Present Stuart Grassie
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

namespace GitHubAPI.Api.Users
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using RestSharp;
    using LoggingExtensions.Logging;

    /// <summary>
    /// Encapsulates access to the Github.com User API.
    /// </summary>
    /// <remarks>
    /// See http://developer.github.com/v3/users/ for more details.
    /// </remarks>
    public static class Users
    {
        private static readonly ILog Log = LoggingExtensions.Logging.Log.GetLoggerFor(typeof (Users).FullName);

        /// <summary>
        /// Get the authenticated user
        /// </summary>
        /// <param name="client">The <see cref="GithubRestApiClient"/> instance to attach to.</param>
        /// <typeparam name="TIssue">The user model to serialise the JSON data into.</typeparam>
        /// <returns>A <see cref="IRestResponse{TIssue}"/> for the authenticated user.</returns>
        public static IRestResponse<T> GetUser<T>(this GithubRestApiClient client) where T : new()
        {
            Log.Info(() => "Making request for the authenticated user.");
            var request = client.RequestFactory.CreateRequest(() => new RestRequest("/user"));

            var response = client.Execute<T>(request);

            return response;
        }

        /// <summary>
        /// Get a single user
        /// </summary>
        /// <param name="client">The <see cref="GithubRestApiClient"/> instance to attach to.</param>
        /// <param name="username">The user to get from GitHub.</param>
        public static IRestResponse<TUser> GetUser<TUser>(this GithubRestApiClient client, string username) where TUser : new()
        {

            var request = client.RequestFactory.CreateRequest("/users/{username}", Method.GET,
                                                              new[]
                                                                  {
                                                                      new KeyValuePair<string, string>("username",
                                                                                                       username)
                                                                  });

            var response = client.Execute<TUser>(request);

            return response;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <remarks>
        /// This provides a dump of every user, in the order that they signed up for GitHub.
        /// </remarks>
        /// <param name="client">The <see cref="GithubRestApiClient"/> instance to attach to.</param>
        /// <param name="id">The integer ID of the last User that you�ve seen.</param>
        /// <returns>A <see cref="IRestResponse"/> containing a chunk of users.</returns>
        public static IRestResponse GetUsers(this GithubRestApiClient client, string id = "")
        {
            var request = client.RequestFactory.CreateRequest(() =>
                                                    {
                                                        var req = new RestRequest("/users?since={id}")
                                                                      {
                                                                          Method = Method.GET
                                                                      };
                                                        req.AddUrlSegment("id",
                                                                          Convert.ToString(id,
                                                                                           CultureInfo.InvariantCulture));
                                                        return req;
                                                    });
            var response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <remarks>
        /// This provides a dump of every user, in the order that they signed up for GitHub.
        /// </remarks>
        /// <param name="client">The <see cref="GithubRestApiClient"/> instance to attach to.</param>
        /// <param name="id">The integer ID of the last User that you�ve seen.</param>
        /// <returns>A <see cref="IRestResponse"/> containing a chunk of users.</returns>
        public static IRestResponse<T> GetUsers<T>(this GithubRestApiClient client, string id = "") where T : new()
        {
            var request = client.RequestFactory.CreateRequest(() =>
                                                                  {
                                                                      var req = new RestRequest("/users?since={id}")
                                                                                    {
                                                                                        Method = Method.GET
                                                                                    };
                                                                      req.AddUrlSegment("id",
                                                                                        Convert.ToString(id,
                                                                                                         CultureInfo
                                                                                                             .InvariantCulture));
                                                                      return req;
                                                                  });
            var response = client.Execute<T>(request);
            return response;
        }

        public static IRestResponse<TUser> UpdateUser<TUser>(this GithubRestApiClient client,
            string name = "", string email = "", string blog = null, string company = "", string location = "", 
            string hireable = "", string bio = "") where TUser : new()
        {
            dynamic data = GetUpdateData(name, email, blog, company, location, hireable, bio);

            var request = client.RequestFactory.CreateRequest(
                () =>
                    {
                        var req = new RestRequest("/user")
                                      {
                                          Method = Method.PATCH,
                                          RequestFormat = DataFormat.Json
                                      };
                        req.AddBody(data);
                        return req;
                    });

            var response = client.Execute<TUser>(request);

            return response;
        }

        private static dynamic GetUpdateData(string name = "", string email = "", string blog = null, string company = "",
                                      string location = "",
                                      string hireable = "", string bio = "")
        {
            dynamic data = new ExpandoObject();
            data.name = !string.IsNullOrEmpty(name) ? name : null;
            data.email = !string.IsNullOrEmpty(email) ? email : null;
            data.blog = !string.IsNullOrEmpty(blog) ? blog : null;
            data.company = !string.IsNullOrEmpty(company) ? company : null;
            data.location = !string.IsNullOrEmpty(location) ? location : null;
            data.hireable = !string.IsNullOrEmpty(hireable) ? (dynamic) bool.Parse(hireable) : null;
            data.bio = !string.IsNullOrEmpty(bio) ? bio : null;

            return data;
        }

        ///// <summary>
        ///// Searches for the user on GitHub.
        ///// </summary>
        ///// <param name="username">The user to search for.</param>
        ///// <returns>Returns a lise of <see cref="GitHubAPI.Models.User"/> instances of GitHub users who may match the search.</returns>
        //public IList<TIssue> SearchUser(string username)
        //{
        //    if (Client == null) Client = GetRestClient();

        //    var request = new RestRequest
        //                      {
        //                          Resource = string.Format("/user/search/{0}", username),
        //                          RootElement = "users"
        //                      };

        //    var response = Client.Execute<List<TIssue>>(request);

        //    return response.Data;
        //}

        ///// <summary>
        ///// Finds a user specified by their email address. 
        ///// This will only match the email address listed in a users public profile, and is opt-in for everyone.
        ///// </summary>
        ///// <param name="email">The email address of the user to search for.</param>
        ///// <returns>A <see cref="GitHubAPI.Models.User"/> instance which encapsulates the response from GitHub for the requested user.</returns>
        //public TIssue FindUserByEmail(string email)
        //{
        //    if (Client == null) Client = GetRestClient();

        //    var request = new RestRequest
        //    {
        //        Resource = string.Format("/user/email/{0}", email),
        //        RootElement = "users"
        //    };

        //    var response = Client.Execute<TIssue>(request);

        //    return response.Data;
        //}

        ///// <summary>
        ///// Gets a list of the users that the specified user is following.
        ///// </summary>
        ///// <param name="username">The username to get the following list for.</param>
        ///// <returns>A list of the users (username only) that the specified user is following.</returns>
        //public IList<TFollowing> GetFollowing<TFollowing>(string username)
        //{
        //    if (Client == null) Client = GetRestClient();

        //    var request = new RestRequest(Method.GET)
        //    {
        //        Resource = "/users/{user}/following"
        //    };
        //    request.AddParameter("user", username, ParameterType.UrlSegment);

        //    var response = Client.Execute<List<TFollowing>>(request);
        //    CheckRateLimit(response.Headers);

        //    response.StatusCode.ShouldBe(HttpStatusCode.OK).IfNotRaiseAnError(response);

        //    return response.Data;
        //}

        ///// <summary>
        ///// Gets a list of the users that the specified user is following.
        ///// </summary>
        ///// <param name="username">The user to get the list of followers for.</param>
        ///// <returns>A string list containing the (username only) list of users who are followers of the specified user.</returns>
        //public IList<TFollowing> GetFollowers<TFollowing>(string username)
        //{
        //    if (Client == null) Client = GetRestClient();

        //    var request = new RestRequest(Method.GET)
        //                      {
        //                          Resource = "/users/{user}/followers"
        //                      };
        //    request.AddParameter("user", username, ParameterType.UrlSegment);

        //    var response = Client.Execute<List<TFollowing>>(request);
        //    CheckRateLimit(response.Headers);

        //    response.StatusCode.ShouldBe(HttpStatusCode.OK).IfNotRaiseAnError(response);

        //    return response.Data;
        //}

        //public bool Follow(string username)
        //{
        //    if (Client == null) Client = GetRestClient();
        //    RequiresAuthentication();

        //    var request = new RestRequest(Method.PUT)
        //                      {
        //                          Resource = "/user/following/{user}"
        //                      };
        //    request.AddParameter("user", username, ParameterType.UrlSegment);
        //    var response = Client.Execute(request);

        //    CheckRateLimit(response.Headers);

        //    return response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        //}

        //public bool UnFollow(string username)
        //{
        //    if (Client == null) Client = GetRestClient();

        //    var request = new RestRequest(Method.DELETE)
        //                      {
        //                          Resource = "/user/following/{user}"
        //                      };
        //    request.AddParameter("user", username, ParameterType.UrlSegment);

        //    var response = Client.Execute(request);

        //    CheckRateLimit(response.Headers);

        //    return response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        //}
    }
}
