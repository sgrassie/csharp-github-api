namespace csharp_github_api
{
    using System;
    using System.Linq;
    using System.Text;
    using RestSharp;

    /// <summary>
    /// A custom implementation of a RestSharp <see cref="IAuthenticator"/>. It is basically the same as RestSharp's
    /// <see cref="HttpBasicAuthenticator" />, but it add's the facility to authenticate with a GitHub api token.
    /// </summary>
    public class GitHubAuthenticator : IAuthenticator
    {
        private readonly string _username;
        private readonly string _password;
        private readonly bool _useApiKey;

        /// <summary>
        /// Instantiates a new instance of the <see cref="GitHubAuthenticator"/> class.
        /// </summary>
        /// <param name="username">The GitHub.com username to login as.</param>
        /// <param name="password">The password for the user. If logging in with an api key, set the password as the token.</param>
        /// <param name="useApiKey">A value which indicates whether or not a GitHub.com api key is being used for authentication.</param>
        public GitHubAuthenticator(string username, string password, bool useApiKey)
        {
            _username = username;
            _password = password;
            _useApiKey = useApiKey;
        }

        /// <summary>
        /// Authenticates the request.
        /// </summary>
        /// <param name="request">The request to authenticate.</param>
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