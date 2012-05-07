//-----------------------------------------------------------------------
// <copyright file="UserApi.cs" company="TemporalCohesion.co.uk">
//     Copyright [2010] [Stuart Grassie]
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

namespace csharp_github_api.Core
{
    using System.Collections.Generic;
    using System.Net;
    using Extensions;
    using Models;
    using RestSharp;

    /// <summary>
    /// Encapsulates access to the Github.com User API.
    /// </summary>
    /// <remarks>
    /// See http://developer.github.com/v3/ for more details.
    /// </remarks>
    public class UserApi : Api
    {
        public UserApi(){}

        /// <summary>
        /// Instantiattes a new instance of the <see cref="UserApi"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url for GitHub's API.</param>
        public UserApi(string baseUrl) : base(baseUrl)
        {
        }

        /// <summary>
        /// Gets the specified user from GitHub.
        /// </summary>
        /// <param name="username">The user to get from GitHub.</param>
        /// <returns>A <see cref="User"/> instance which encapsulates the response from GitHub for the requested user.</returns>
        public User GetUser(string username)
        {
            if (Client == null) Client = GetRestClient();

            var request = new RestRequest
                              {
                                  Resource = string.Format("/users/{0}", username)
                              };

            var response = Client.Execute<User>(request);

            CheckRateLimit(response.Headers);

            var user = response.Data;

            return user;
        }

        /// <summary>
        /// Searches for the user on GitHub.
        /// </summary>
        /// <param name="username">The user to search for.</param>
        /// <returns>Returns a lise of <see cref="User"/> instances of GitHub users who may match the search.</returns>
        public IList<User> SearchUser(string username)
        {
            if (Client == null) Client = GetRestClient();

            var request = new RestRequest
                              {
                                  Resource = string.Format("/user/search/{0}", username),
                                  RootElement = "users"
                              };

            var response = Client.Execute<List<User>>(request);

            return response.Data;
        }

        /// <summary>
        /// Finds a user specified by their email address. 
        /// This will only match the email address listed in a users public profile, and is opt-in for everyone.
        /// </summary>
        /// <param name="email">The email address of the user to search for.</param>
        /// <returns>A <see cref="User"/> instance which encapsulates the response from GitHub for the requested user.</returns>
        public User FindUserByEmail(string email)
        {
            if (Client == null) Client = GetRestClient();

            var request = new RestRequest
            {
                Resource = string.Format("/user/email/{0}", email),
                RootElement = "users"
            };

            var response = Client.Execute<User>(request);

            return response.Data;
        }

        /// <summary>
        /// Gets a list of the users that the specified user is following.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to get the following list for.</param>
        /// <returns>A list of the users (username only) that the specified user is following.</returns>
        public IList<Following> GetFollowing(User user)
        {
            return GetFollowing(user.Login);
        }

        /// <summary>
        /// Gets a list of the users that the specified user is following.
        /// </summary>
        /// <param name="username">The username to get the following list for.</param>
        /// <returns>A list of the users (username only) that the specified user is following.</returns>
        public IList<Following> GetFollowing(string username)
        {
            if (Client == null) Client = GetRestClient();

            var request = new RestRequest(Method.GET)
            {
                Resource = "/users/{user}/following"
            };
            request.AddParameter("user", username, ParameterType.UrlSegment);

            var response = Client.Execute<List<Following>>(request);
            CheckRateLimit(response.Headers);

            response.StatusCode.ShouldBe(HttpStatusCode.OK).IfNotRaiseAnError(response);

            return response.Data;
        }

        /// <summary>
        /// Gets a list of the users that the specified user is following.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to get the list of followers for.</param>
        /// <returns>A string list containing the (username only) list of users who are followers of the specified user.</returns>
        public IList<Following> GetFollowers(User user)
        {
            return GetFollowers(user.Login);
        }

        /// <summary>
        /// Gets a list of the users that the specified user is following.
        /// </summary>
        /// <param name="username">The user to get the list of followers for.</param>
        /// <returns>A string list containing the (username only) list of users who are followers of the specified user.</returns>
        public IList<Following> GetFollowers(string username)
        {
            if (Client == null) Client = GetRestClient();

            var request = new RestRequest(Method.GET)
                              {
                                  Resource = "/users/{user}/followers"
                              };
            request.AddParameter("user", username, ParameterType.UrlSegment);

            var response = Client.Execute<List<Following>>(request);
            CheckRateLimit(response.Headers);

            response.StatusCode.ShouldBe(HttpStatusCode.OK).IfNotRaiseAnError(response);

            return response.Data;
        }

        public bool Follow(string username)
        {
            if (Client == null) Client = GetRestClient();
            RequiresAuthentication();

            var request = new RestRequest(Method.PUT)
                              {
                                  Resource = "/user/following/{user}"
                              };
            request.AddParameter("user", username, ParameterType.UrlSegment);
            var response = Client.Execute(request);

            CheckRateLimit(response.Headers);

            return response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        public bool UnFollow(string username)
        {
            if (Client == null) Client = GetRestClient();

            var request = new RestRequest(Method.DELETE)
                              {
                                  Resource = "/user/following/{user}"
                              };
            request.AddParameter("user", username, ParameterType.UrlSegment);

            var response = Client.Execute(request);

            CheckRateLimit(response.Headers);

            return response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
