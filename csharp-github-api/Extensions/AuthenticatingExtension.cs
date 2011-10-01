//-----------------------------------------------------------------------
// <copyright file="AuthenticatingExtension.cs" company="TemporalCohesion.co.uk">
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

namespace csharp_github_api.Extensions
{
    using Authentication;
    using Core;
    using RestSharp;

    /// <summary>
    /// Extension methods for authentication.
    /// </summary>
    public static class AuthenticatingExtension
    {
        /// <summary>
        /// Intercept the TTarget and add the authenticator to it so that calls to Github are authenticated on the fly.
        /// </summary>
        /// <typeparam name="TTarget">The type of <see cref="Api"/> object intercept.</typeparam>
        /// <param name="target">The <see cref="TTarget"/> instance to intercept.</param>
        /// <param name="authenticator">The RestSharp <see cref="IAuthenticator"/> which will be added to the intercepted
        /// object.</param>
        /// <returns>A Dynamic Proxy for the <see cref="TTarget"/> which now has authentication enabled.</returns>
        public static TTarget WithAuthentication<TTarget>(this TTarget target, IAuthenticator authenticator)
            where TTarget : Api
        {
            return AuthenticatingInterceptor<TTarget>.Create(target, authenticator);
        }

        /// <summary>
        /// Intercept the TTarget and add a <see cref="HttpBasicAuthenticator"/> to it so that calls to Github are authenticated on the fly.
        /// </summary>
        /// <typeparam name="TTarget">The type of <see cref="Api"/> object intercept.</typeparam>
        /// <param name="target">The <see cref="TTarget"/> instance to intercept.</param>
        /// <param name="username">The username to authenticate as.</param>
        /// <param name="password">The password to authenticate with.</param>
        /// <returns>A Dynamic Proxy for the <see cref="TTarget"/> which now has authentication enabled.</returns>
        public static TTarget WithAuthentication<TTarget>(this TTarget target, string username, string password)
            where TTarget : Api
        {
            IAuthenticator authenticator = new HttpBasicAuthenticator(username, password);
            return AuthenticatingInterceptor<TTarget>.Create(target, authenticator);
        }
    }
}
