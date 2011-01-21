using System.Net;
using RestSharp;

namespace csharp_github_api.Core
{
    /// <summary>
    /// Base class for specific API classes.
    /// </summary>
    public abstract class Api
    {
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
    }
}
