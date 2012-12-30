//-----------------------------------------------------------------------
// <copyright file="RestRequestFactory.cs" company="TemporalCohesion.co.uk">
//     Copyright 2012 - Present Stuart Grassie
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

namespace csharp_github_api.Resource
{
    using Core;
    using RestSharp;
    using System;
    using System.Collections.Generic;

    public class RestRequestFactory : IRestRequestFactory
    {
        private static IHeaderProvider _headerProvider;

        public RestRequestFactory(IHeaderProvider headerProvider = null)
        {
            _headerProvider = headerProvider;
        }

        public IRestRequest CreateRequest(Func<IRestRequest> request)
        {
            return request.Invoke();
        }

        public IRestRequest CreateRequest(string resource, Method method = Method.GET, params Parameter[] parameters)
        {
            return CreateRequest(
                () =>
                    {
                        IRestRequest request = new RestRequest
                                          {
                                              Resource = resource,
                                              Method = method
                                          };

                        if (parameters.Length == 0) return request;

                        foreach (var parameter in parameters)
                        {
                            request.AddParameter(parameter);
                        }

                        if (_headerProvider != null)
                            _headerProvider.PopulateHeaders(ref request);

                        return request;
                    }
                );
        }

        public IRestRequest CreateRequest(string resource, Method method = Method.GET, params KeyValuePair<string, string>[] parameters)
        {
            return CreateRequest(
                () =>
                    {
                        IRestRequest request = new RestRequest
                                          {
                                              Resource = resource,
                                              Method = method
                                          };

                        if (parameters.Length == 0) return request;

                        foreach (var kvp in parameters)
                        {
                            request.AddUrlSegment(kvp.Key, kvp.Value);
                        }

                        if (_headerProvider != null)
                            _headerProvider.PopulateHeaders(ref request);

                        return request;
                    }
                );
        }
    }
}