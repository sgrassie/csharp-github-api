//-----------------------------------------------------------------------
// <copyright file="MiscExtensionscs" company="TemporalCohesion.co.uk">
//     Copyright [2011] [Stuart Grassie]
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

using System.Collections.Generic;
using RestSharp;
using csharp_github_api.Exceptions;

namespace csharp_github_api.Extensions
{
    using System.Net;

    /// <summary>
    /// Misc handy extensions
    /// </summary>
    public static class MiscExtensions
    {
        /// <summary>
        /// The <see cref="HttpStatusCode"/> should be the one we expect.
        /// </summary>
        /// <param name="response">The actual status code returned from the response.</param>
        /// <param name="expected">The expected status code.</param>
        /// <returns><c>True</c> if the response is the expected one; otherwise <c>False</c>.</returns>
        public static bool ShouldBe(this HttpStatusCode response, HttpStatusCode expected)
        {
            return response.Equals(expected);
        }

        public static void IfNotRaiseAnError<TModel>(this bool ok, IRestResponse<List<TModel>> response)
        {
            if (ok) return;

            var exception = new GitHubResponseException(response.ErrorMessage)
                                {
                                    Response = response
                                };

            throw exception;
        }
    }
}
