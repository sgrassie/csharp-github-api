using System;
using System.Collections.Generic;
using RestSharp;

namespace csharp_github_api.Resource
{
    public interface IRestRequestFactory
    {
        IRestRequest CreateRequest(Func<IRestRequest> request);
        IRestRequest CreateRequest(string resource, Method method = Method.GET, params Parameter[] parameters);
        IRestRequest CreateRequest(string resource, Method method = Method.GET, params KeyValuePair<string, string>[] parameters);
    }
}