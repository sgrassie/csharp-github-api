//-----------------------------------------------------------------------
// <copyright file="AuthenticatingInterceptor.cs" company="TemporalCohesion.co.uk">
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

namespace csharp_github_api.Authentication
{
    using System;
    using System.Reflection;
    using Castle.DynamicProxy;
    using Core;
    using RestSharp;

    public static class AuthenticatingInterceptor<TTarget>
        where TTarget : Api
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public static TTarget Create(TTarget target, IAuthenticator authenticator)
        {
            return ProxyGenerator.CreateClassProxyWithTarget(target,
                                                                new ProxyGenerationOptions(
                                                                    new AuthenticatingProxyGenerationHook()),
                                                                    new AuthenticatorInterceptor(authenticator));
        }

        private class AuthenticatorInterceptor : IInterceptor
        {
            private readonly IAuthenticator _authenticator;

            public AuthenticatorInterceptor(IAuthenticator authenticator)
            {
                _authenticator = authenticator;
            }

            public void Intercept(IInvocation invocation)
            {
                invocation.Proceed(); // let the RestClient be instantiated as normal.
                var restClient = (RestClient)invocation.ReturnValue;
                restClient.Authenticator = _authenticator; // add the authenticator
                invocation.ReturnValue = restClient; 
            }
        }
    }

    public class AuthenticatingProxyGenerationHook : IProxyGenerationHook
    {
        /// <summary>
        /// Invoked by the generation process to determine if the specified method should be proxied.
        /// </summary>
        /// <param name="type">The type which declares the given method.</param><param name="methodInfo">The method to inspect.</param>
        /// <returns>
        /// True if the given method should be proxied; false otherwise.
        /// </returns>
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            return methodInfo.Name.Equals("GetRestClient");
        }

        /// <summary>
        /// Invoked by the generation process to notify that a member was not marked as virtual.
        /// </summary>
        /// <param name="type">The type which declares the non-virtual member.</param><param name="memberInfo">The non-virtual member.</param>
        /// <remarks>
        /// This method gives an opportunity to inspect any non-proxyable member of a type that has 
        ///             been requested to be proxied, and if appropriate - throw an exception to notify the caller.
        /// </remarks>
        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            if(memberInfo.Name == "GetRestClient")
            {
                throw new ProxyGenerationException("Can't proxy it man.");
            }
        }

        /// <summary>
        /// Invoked by the generation process to notify that the whole process has completed.
        /// </summary>
        public void MethodsInspected()
        {
        }
    }
}
