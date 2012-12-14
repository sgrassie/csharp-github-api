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
    using System;
    using Core;
    using RestSharp;

    /// <summary>
    /// Access the Github.com API.
    /// </summary>
    public class Github
    {
        private readonly IAuthenticator _gitHubAuthenticator;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Github()
        {
            
        }

        /// <summary>
        /// Instantiates a new instance of the Github class.
        /// </summary>
        /// <param name="authenticator">The <see cref="IAuthenticator"/> to use for authentication.</param>
        public Github(IAuthenticator authenticator)
        {
            _gitHubAuthenticator = authenticator;
        }

        /// <summary>
        /// Gets or sets the base URL for accessing GitHub's API.
        /// </summary>
        public string BaseUrl { get; set; }

        public UserApi<TUser> User<TUser>() where TUser : new()
        {
            return new UserApi<TUser>(BaseUrl);
        }
    }
}
