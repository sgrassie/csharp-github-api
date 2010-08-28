using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kayak.Framework;
using System.Net;
using System.IO;

namespace csharp_github_api.IntegrationTests.Services
{
    /// <summary>
    /// json data taken from: http://develop.github.com/p/users.html 
    /// </summary>
    public class UserService : KayakService
    {
        private const string DefunktJson =
            "{\"user\": {" +
                "\"id\": 23, " +
                "\"login\": \"defunkt\", " +
                "\"name\": \"Kristopher Walken Wanstrath\", " +
                "\"company\": \"LA\", " +
                "\"location\": \"SF\", " +
                "\"email\": \"me@email.com\", " +
                "\"blog\": \"http://myblog.com\", " +
                "\"following_count\": 13, " +
                "\"followers_count\": 63, " +
                "\"public_gist_count\": 0, " +
                "\"public_repo_count\": 2}}";

        [Path("/user/show/{username}")]
        public void Show(string username)
        {
            if (Response.Headers["Authenticated"].Equals("true"))
            {
                if (username == "sgrassie" || username == "defunkt")
                {
                    Response.Headers.Add("Content-Type", "application/json");

                    Response.Write(DefunktJson);
                }
            }
        }

        [Path("/user/show/{username}/following")]
        public void Following(string username)
        {
            Response.Headers.Add("Content-Type", "application/json");
            Response.Write("{\"users\":[\"johnsheehan\",\"jagregory\",\"drusellers\",\"structuremap\"]}");
        }
    }
}
