using System;
using System.Linq;
using System.Text;
using RestSharp;

namespace csharp_github_api
{
    public class GitHubAuthenticator : IAuthenticator
    {
        private readonly string _username;
        private readonly string _password;
        private readonly bool _useApiKey;

        public GitHubAuthenticator(string username, string password, bool useApiKey)
        {
            _username = username;
            _password = password;
            _useApiKey = useApiKey;
        }

        public void Authenticate(RestRequest request)
        {
            if (!request.Parameters.Any(p => p.Name.Equals("Authorization", StringComparison.InvariantCultureIgnoreCase)))
            {
                string token;

                if (_useApiKey)
                {
                    token =
                        Convert.ToBase64String(
                            Encoding.UTF8.GetBytes(string.Format("{0}/token:{1}", _username, _password)));
                }
                else
                {
                    token =
                        Convert.ToBase64String(
                            Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _username, _password)));
                }

                var authHeader = string.Format("Basic {0}", token);
                request.AddParameter("Authorization", authHeader, ParameterType.HttpHeader);
            }
        }
    }
}