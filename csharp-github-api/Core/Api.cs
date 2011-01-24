//-----------------------------------------------------------------------
// <copyright file="Api.cs" company="TemporalCohesion.co.uk">
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

namespace csharp_github_api.Core
{
    using System.Net;
    using RestSharp;

    /// <summary>
    /// Base class for specific API classes.
    /// </summary>
    public abstract class Api
    {
        public string BaseUrl;
        protected RestClient Client;
        protected IAuthenticator Authenticator;

        /// <summary>
        /// Instantiattes a new instance of the <see cref="Api"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url for GitHub's API.</param>
        protected Api(string baseUrl)
        {
            BaseUrl = baseUrl;
            Client = new RestClient(BaseUrl);
        }

        /// <summary>
        /// Instantiattes a new instance of the <see cref="Api"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url for GitHub's API.</param>
        /// <param name="authenticator">The <see cref="IAuthenticator"/> class to use to authenticate requests to the user api.</param>
        protected Api(string baseUrl, IAuthenticator authenticator)
        {
            BaseUrl = baseUrl;
            Authenticator = authenticator;
            Client = new RestClient(BaseUrl);
        }

        public virtual void ThrowExceptionForBadResponseIfNeccessary(RestResponseBase response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var message = response.Content;

                var exception = new GitHubResponseException(message)
                                    {
                                        Response = response
                                    };

                throw exception;
            }
        }

        protected virtual RestClient GetRestClient()
        {
            return new RestClient(BaseUrl);
        }
    }
}
