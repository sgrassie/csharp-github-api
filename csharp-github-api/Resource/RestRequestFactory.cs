using System;
using System.Collections.Generic;
using RestSharp;

namespace csharp_github_api.Resource
{
    public class RestRequestFactory
    {
        private RestRequestFactory()
        {
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
                        var request = new RestRequest
                                          {
                                              Resource = resource,
                                              Method = method
                                          };

                        if (parameters.Length == 0) return request;

                        foreach (var parameter in parameters)
                        {
                            request.AddParameter(parameter);
                        }

                        return request;
                    }
                );
        }

        public static IRestRequest CreateRequest(string resource, Method method = Method.GET, params KeyValuePair<string, string>[] parameters)
        {
            return CreateRequest(
                () =>
                    {
                        var request = new RestRequest
                                          {
                                              Resource = resource,
                                              Method = method
                                          };

                        if (parameters.Length == 0) return request;

                        foreach (var kvp in parameters)
                        {
                            request.AddUrlSegment(kvp.Key, kvp.Value);
                        }

                        return request;
                    }
                );
        }
    }
}