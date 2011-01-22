//-----------------------------------------------------------------------
// <copyright file="Github.cs" company="TemporalCohesion.co.uk">
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

using csharp_github_api.Core;

namespace csharp_github_api
{
    using RestSharp;
    using Models;

    /// <summary>
    /// Access the Github.com API.
    /// </summary>
    public class Github
    {
        private readonly IAuthenticator _gitHubAuthenticator;
        private string _baseUrl;

        private UserApi _userApi;

        /// <summary>
        /// Instantiates a new instance of the <see cref="Github"/> class.
        /// </summary>
        /// <param name="baseurl">The base url for GitHub's API.</param>
        public Github(string baseurl)
        {
            _baseUrl = baseurl;
            Init();
        }

        /// <summary>
        /// Instantiates a new instance of the <see cref="Github"/> class.
        /// </summary>
        /// <param name="baseurl">The base url for GitHub's API.</param>
        /// <param name="passwordFile">The path to an xml file which contains the Github.com login information for the account to use.</param>
        public Github(string baseurl, string passwordFile) : this(baseurl)
        {
            var secrets = new SecretsHandler(passwordFile);

            if(string.IsNullOrEmpty(secrets.Token))
            {
                _gitHubAuthenticator = new GitHubAuthenticator(secrets.Username, secrets.Password, false);
            }
            else
            {
                _gitHubAuthenticator = new GitHubAuthenticator(secrets.Username, secrets.Token, true);
            }
        }

        /// <summary>
        /// Instantiates a new instance of the <see cref="Github"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url for GitHub's API.</param>
        /// <param name="authenticator">The <see cref="IAuthenticator"/> to use to authenticate requests to Github.com</param>
        public Github(string baseUrl, IAuthenticator authenticator) : this(baseUrl)
        {
            _gitHubAuthenticator = authenticator;
        }

        /// <summary>
        /// Instantiates a new instance of the <see cref="Github"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url for GitHub's API.</param>
        /// <param name="username">The username to authenticate as.</param>
        /// <param name="password">The password for the user. If authenticating with an API token, pass the token instead of the password.</param>
        /// <param name="useApiKey">Indicates whether or not to use a Github.com API token. If <c>true</c>, then pass the token instead of the password.</param>
        public Github(string baseUrl, string username, string password, bool useApiKey) : this(baseUrl)
        {
            _gitHubAuthenticator = new GitHubAuthenticator(username, password, useApiKey);
        }

        private void Init()
        {
            _userApi = new UserApi(BaseUrl);
        }

        /// <summary>
        /// Gets or sets the base URL for accessing GitHub's API.
        /// </summary>
        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        public UserApi User
        {
            get
            {
                return _userApi;
            }
        }
    }
}
