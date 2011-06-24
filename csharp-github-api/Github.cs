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

namespace csharp_github_api
{
    using RestSharp;
    using csharp_github_api.Core;

    /// <summary>
    /// Access the Github.com API.
    /// </summary>
    public class Github
    {
        private readonly IAuthenticator _gitHubAuthenticator;
        private string _baseUrl;

        /// <summary>
        /// Instantiates a new instance of the <see cref="Github"/> class.
        /// </summary>
        /// <param name="baseurl">The base url for GitHub's API.</param>
        public Github(string baseurl)
        {
            _baseUrl = baseurl;
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
                return new UserApi(_baseUrl, _gitHubAuthenticator);
            }
        }
    }
}
