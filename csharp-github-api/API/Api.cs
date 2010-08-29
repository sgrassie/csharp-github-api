using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using RestSharp;
using RestSharp.Contrib;

namespace csharp_github_api.API
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
