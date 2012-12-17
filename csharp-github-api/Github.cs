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

using System;

namespace csharp_github_api
{
    using RestSharp;
    using Logging;

    /// <summary>
    /// Access the Github.com API.
    /// </summary>
    public class Github
    {
        private readonly IAuthenticator _gitHubAuthenticator;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="baseUrl">The base URL for accessing GitHub's API.</param>
        public Github(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public Github WithLogger(Func<Type, ILog> logger)
        {
            LogManager.GetLog = logger;
            
            this.Log().Debug(() => "Initialised with Logger.");
            return this;
        }

        /// <summary>
        /// Instantiates a new instance of the Github class.
        /// </summary>
        /// <param name="baseUrl">The base URL for accessing GitHub's API.</param>
        /// <param name="authenticator">The <see cref="IAuthenticator"/> to use for authentication.</param>
        public Github(string baseUrl, IAuthenticator authenticator)
            : this(baseUrl)
        {
            _gitHubAuthenticator = authenticator;
        }

        /// <summary>
        /// Gets or sets the base URL for accessing GitHub's API.
        /// </summary>
        public string BaseUrl { get; set; }

        public Users User()
        {
            return new Users(BaseUrl);
        }
    }
}
