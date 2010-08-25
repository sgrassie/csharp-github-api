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
        public readonly string BaseUrl;

        private RestClient _client;
        private IAuthenticator _authenticator;

        public UserApi(string baseUrl)
        {
            BaseUrl = baseUrl;

            _client = new RestClient(BaseUrl);
        }

        public UserApi Authenticated(IAuthenticator authenticator)
        {
            _authenticator = authenticator;

            _client = GetRestClient();
            _client.Authenticator = authenticator;

            return this;
        }

        public User GetUser(string username)
        {
            if (_client == null) _client = GetRestClient();

            var request = new RestRequest
                              {
                                  Resource = string.Format("/user/show/{0}", username),
                                  RootElement = "user"
            };

            var response = _client.Execute<User>(request);

            var user = response.Data;
            user.Api = this;

            return user;
        }

        private RestClient GetRestClient()
        {
            return new RestClient(BaseUrl);
        }
    }
}
