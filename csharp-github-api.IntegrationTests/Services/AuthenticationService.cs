using System;
using System.Text;
using Kayak.Framework;

namespace csharp_github_api.IntegrationTests.Services
{
    public class AuthenticationService : KayakService
    {
        //[Path("/user/show/{username}")]
        public void Authenticated(string username)
        {
            var header = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(header))
            {
                Response.Headers.Add("authenticated", "false");
            }
            else
            {
                //var parts = Encoding.ASCII.GetString(Convert.FromBase64String(header.Substring("Basic ".Length))).Split(':');
                Response.Headers.Add("authenticated", "true");
            }
        }
    }
}
