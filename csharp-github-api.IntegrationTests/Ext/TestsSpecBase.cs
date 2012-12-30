using System;
using System.Configuration;
using RestSharp;

namespace csharp_github_api.IntegrationTests.Ext
{
    public abstract class TestsSpecBase : TinySpec
    {
        protected const string GitHubUrl = @"https://api.github.com";

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Token { get; private set; }

        public override void Context()
        {
            var config = ConfigurationManager.OpenExeConfiguration("csharp-github-api.IntegrationTests.dll");
            Username = config.AppSettings.Settings["username"].Value;
            Password = config.AppSettings.Settings["password"].Value;
            Token = config.AppSettings.Settings["token"].Value;
        }

        /// <summary>
        /// By default returns a <see cref="HttpBasicAuthenticator"/>. Override to return a different implementation.
        /// </summary>
        /// <returns>A <see cref="HttpBasicAuthenticator"/></returns>
        public virtual IAuthenticator Authenticator()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password)
                       ? new HttpBasicAuthenticator(Username, Password)
                       : null;
        }
    }
}
