using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using csharp_github_api.Models;

namespace csharp_github_api.API
{
    /// <summary>
    /// Encapsulates access to the Github.com User API.
    /// </summary>
    /// <remarks>
    /// See http://develop.github.com/p/users.html for more details.
    /// </remarks>
    public class UserApi
    {
        private readonly string _baseUrl;
        private readonly IAuthenticator _authenticator;

        public UserApi(string baseUrl, IAuthenticator authenticator)
        {
            _baseUrl = baseUrl;
            _authenticator = authenticator;
        }

        public User GetUser(string username)
        {
            var client = new RestClient(_baseUrl) {Authenticator = _authenticator};

            var request = new RestRequest
                              {
                                  Resource = string.Format("/user/show/{0}", username),
                                  RootElement = "user"
            };

            var response = client.Execute<User>(request);

            return response.Data;
        }
    }
}
