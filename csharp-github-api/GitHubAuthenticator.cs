//-----------------------------------------------------------------------
// <copyright file="GitHubAuthenticator.cs" company="TemporalCohesion.co.uk">
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
    using System;
    using System.Linq;
    using System.Text;
    using RestSharp;
using csharp_github_api.Framework;

    /// <summary>
    /// A custom implementation of a RestSharp <see cref="IAuthenticator"/>. It is basically the same as RestSharp's
    /// <see cref="HttpBasicAuthenticator" />, but it add's the facility to authenticate with a GitHub api token.
    /// </summary>
    public class GitHubAuthenticator : IAuthenticator
    {
        private readonly string _username;
        private readonly string _password;
        private readonly bool _useApiKey;

        /// <summary>
        /// Instantiates a new instance of the <see cref="GitHubAuthenticator"/> class.
        /// </summary>
        /// <param name="username">The GitHub.com username to login as.</param>
        /// <param name="password">The password for the user. If logging in with an api key, set the password as the token.</param>
        /// <param name="useApiKey">A value which indicates whether or not a GitHub.com api key is being used for authentication.</param>
        public GitHubAuthenticator(string username, string password, bool useApiKey)
        {
            _username = username;
            _password = password;
            _useApiKey = useApiKey;
        }

        /// <summary>
        /// Instantiates a new instance of the <see cref="GitHubAuthenticator"/> class.
        /// </summary>
        /// <param name="gitHubApiSettings">The <see cref="IGitHubApiSettings"/> settings provider.</param>
        public GitHubAuthenticator(IGitHubApiSettings gitHubApiSettings)
        {
            _username = gitHubApiSettings.Username;
            if(!string.IsNullOrEmpty(gitHubApiSettings.Password))
            {
                _password = gitHubApiSettings.Password;
            }
            else
            {
                _password = gitHubApiSettings.Token;
                _useApiKey = true;
            }
        }

        /// <summary>
        /// Authenticates the request.
        /// </summary>
        /// <param name="request">The request to authenticate.</param>
        public void Authenticate(RestRequest request)
        {
            if (!request.Parameters.Any(p => p.Name.Equals("Authorization", StringComparison.InvariantCultureIgnoreCase)))
            {
                string token;

                if (_useApiKey)
                {
                    token =
                        Convert.ToBase64String(
                            Encoding.UTF8.GetBytes(string.Format("{0}/token:{1}", _username, _password)));
                }
                else
                {
                    token =
                        Convert.ToBase64String(
                            Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _username, _password)));
                }

                var authHeader = string.Format("Basic {0}", token);
                request.AddParameter("Authorization", authHeader, ParameterType.HttpHeader);
            }
        }
    }
}