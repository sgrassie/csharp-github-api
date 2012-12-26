using System;
using System.Collections.Generic;
using RestSharp;
using csharp_github_api.Core;

namespace csharp_github_api.Resource
{
    public class RestRequestFactory
    {
        private static IHeaderProvider _headerProvider;

        private RestRequestFactory(IHeaderProvider headerProvider = null)
        {
            _headerProvider = headerProvider;
        }

        public static IRestRequest CreateRequest(Func<IRestRequest> request)
        {
            return request.Invoke();
        }

        public static IRestRequest CreateRequest(string resource, Method method = Method.GET, params Parameter[] parameters)
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

        public static IRestRequest CreateRequest(string resource, Method method = Method.GET, params KeyValuePair<string, string>[] parameters)
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