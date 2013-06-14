//-----------------------------------------------------------------------
// <copyright file="Api.cs" company="TemporalCohesion.co.uk">
//     Copyright 2010 - Present Stuart Grassie
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

namespace GitHubAPI
{
    using Exceptions;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Base class for specific API classes.
    /// </summary>
    public abstract class ApiBase
    {
        public string BaseUrl;
        protected IAuthenticator Authenticator;
        private int _rateLimitRemaining;

        protected ApiBase(){}

        /// <summary>
        /// Instantiattes a new instance of the <see cref="Api"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url for GitHub's API.</param>
        protected ApiBase(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// Gets an integer which holds the API rate limit.
        /// </summary>
        public virtual int RateLimit
        {
            get; private set;
        }

        /// <summary>
        /// Gets an integer which holds the remaining count of API requests calls
        /// </summary>
        public virtual int RateLimitRemaining
        {
            get { return _rateLimitRemaining; }
            private set 
            { 
                _rateLimitRemaining = value; 

                Debug.WriteLine(string.Format("Current remaining rate limit: {0}", _rateLimitRemaining));

                if (_rateLimitRemaining <= 0)
                    throw new GitHubResponseException(string.Format("Github API rate limit ({0}) has been reached.", RateLimit));
            }
        }

        protected virtual void CheckRateLimit(IEnumerable<Parameter> headers)
        {
            var rateLimits = headers.AsQueryable().Where(x => x.Name.StartsWith("X-RateLimit"));
            //these aren't there in the case of an access error
            var actualRateLimit = rateLimits.SingleOrDefault(x => x.Name.EndsWith("-Limit"));
            if (actualRateLimit != null)
            {
                RateLimit = Convert.ToInt32(actualRateLimit.Value);
            }

            var remainingRateLimit = rateLimits.SingleOrDefault(x => x.Name.EndsWith("-Remaining"));
            if (remainingRateLimit != null)
            {
                RateLimitRemaining = Convert.ToInt32(remainingRateLimit.Value);
            }
        }
    }
}
