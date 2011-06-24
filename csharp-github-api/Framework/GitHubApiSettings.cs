//-----------------------------------------------------------------------
// <copyright file="GitHubApiSettings.cs" company="TemporalCohesion.co.uk">
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

namespace csharp_github_api.Framework
{
    /// <summary>
    /// Default implementation of the settings provider for the API. 
    /// A simple POCO class which provides settings to the rest of the C# Github API.
    /// </summary>
    public class GitHubApiSettings : IGitHubApiSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubApiSettings"/> class.
        /// </summary>
        /// <remarks>
        /// Defaults <see cref="BaseUrl"/> to the current Github v2 API JSON URL.
        /// </remarks>
        public GitHubApiSettings()
        {
            BaseUrl = "https://api.github.com";    
        }

        /// <summary>
        /// Gets or sets the username for Github.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for Github. If <see cref="Token"/> is set, this setting is ignoredl
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the users Github API token. 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the base url to use for Github. Only use the Github JSON API.
        /// </summary>
        public string BaseUrl { get; set; }
    }
}
