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
    using csharp_github_api.Resource;
    using LoggingExtensions.Logging;
    using RestSharp;
    using System;

    /// <summary>
    /// Access the Github.com API.
    /// </summary>
    public partial class GithubRestApiClient : Api
    {
        private IAuthenticator _gitHubAuthenticator;
        private IRestRequestFactory _restRequestFactory;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="baseUrl">The base URL for accessing GitHub's API.</param>
        public GithubRestApiClient(string baseUrl = "https://api.github.com")
        {
            BaseUrl = baseUrl;
            Log.InitializeWith<NullLog>();
        }

        public GithubRestApiClient WithLogger<TLogger>() where TLogger : ILog, new()
        {
            Log.InitializeWith<TLogger>();
            this.Log().Info("Logging enabled with {0}.", typeof(TLogger).FullName);

            return this;
        }

        /// <summary>
        /// Add authentication to the client.
        /// </summary>
        /// <param name="authenticator">The <see cref="IAuthenticator"/> to use for authentication.</param>
        public GithubRestApiClient WithAuthentication(IAuthenticator authenticator)
        {
            _gitHubAuthenticator = authenticator;
            return this;
        }
    }
}
