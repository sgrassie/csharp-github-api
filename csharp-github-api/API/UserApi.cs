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

namespace csharp_github_api.API
{
    using RestSharp;
    using Models;

    /// <summary>
    /// Encapsulates access to the Github.com User API.
    /// </summary>
    /// <remarks>
    /// See http://develop.github.com/p/users.html for more details.
    /// </remarks>
    public class UserApi
    {
        private readonly string _baseUrl;
        private readonly IAuthenticator _authenticator;

        public UserApi(string baseUrl, IAuthenticator authenticator)
        {
            _baseUrl = baseUrl;
            _authenticator = authenticator;
        }

        public User GetUser(string username)
        {
            var client = new RestClient(_baseUrl) {Authenticator = _authenticator};

            var request = new RestRequest
                              {
                                  Resource = string.Format("/user/show/{0}", username),
                                  RootElement = "user"
            };

            var response = client.Execute<User>(request);

            return response.Data;
        }
    }
}
