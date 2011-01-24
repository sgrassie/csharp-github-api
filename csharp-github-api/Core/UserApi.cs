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


using System;
using RestSharp;
using csharp_github_api.Models;
using System.Collections.Generic;

namespace csharp_github_api.Core
{
    /// <summary>
    /// Encapsulates access to the Github.com User API.
    /// </summary>
    /// <remarks>
    /// See http://develop.github.com/p/users.html for more details.
    /// </remarks>
    public class UserApi : Api
    {
        /// <summary>
        /// Instantiattes a new instance of the <see cref="UserApi"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url for GitHub's API.</param>
        public UserApi(string baseUrl) : base(baseUrl)
        {
        }

        /// <summary>
        /// Instantiattes a new instance of the <see cref="UserApi"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url for GitHub's API.</param>
        /// <param name="authenticator">The <see cref="IAuthenticator"/> class to use to authenticate requests to the user api.</param>
        public UserApi(string baseUrl, IAuthenticator authenticator) : base(baseUrl, authenticator)
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
                                  Resource = string.Format("/user/show/{0}", username),
                                  RootElement = "user"
                              };

            var response = Client.Execute<User>(request);

            var user = response.Data;
            user.UserApi = this;

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

        internal UserApi Authenticated()
        {
            if (Authenticator != null)
            {
                Client = GetRestClient();
                Client.Authenticator = Authenticator;
            }

            return this;
        }

        /// <summary>
        /// Gets a list of the users that the specified user is following.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to get the following list for.</param>
        /// <returns>A list of the users (username only) that the specified user is following.</returns>
        internal IList<string> GetFollowing(User user)
        {
            return GetFollowing(user.Login);
        }

        /// <summary>
        /// Gets a list of the users that the specified user is following.
        /// </summary>
        /// <param name="username">The username to get the following list for.</param>
        /// <returns>A list of the users (username only) that the specified user is following.</returns>
        internal IList<string> GetFollowing(string username)
        {
            if (Client == null) Client = GetRestClient();

            var request = new RestRequest
            {
                Resource = string.Format("/user/show/{0}/following", username),
                RootElement = "users"
            };

            var response = Client.Execute<List<string>>(request);

            return response.Data;
        }

        /// <summary>
        /// Gets a list of the users that the specified user is following.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to get the list of followers for.</param>
        /// <returns>A string list containing the (username only) list of users who are followers of the specified user.</returns>
        internal IList<string> GetFollowers(User user)
        {
            return GetFollowers(user.Login);
        }

        /// <summary>
        /// Gets a list of the users that the specified user is following.
        /// </summary>
        /// <param name="username">The user to get the list of followers for.</param>
        /// <returns>A string list containing the (username only) list of users who are followers of the specified user.</returns>
        internal IList<string> GetFollowers(string username)
        {
            if (Client == null) Client = GetRestClient();

            var request = new RestRequest
                              {
                                  Resource = string.Format("/user/show/{0}/followers", username),
                                  RootElement = "users"
                              };

            var response = Client.Execute<List<string>>(request);
            ThrowExceptionForBadResponseIfNeccessary(response);

            return response.Data;
        }

        internal bool Follow(string username)
        {
            throw new NotImplementedException();
        }

        public bool UnFollow(string username)
        {
            throw new NotImplementedException();
        }
    }
}
