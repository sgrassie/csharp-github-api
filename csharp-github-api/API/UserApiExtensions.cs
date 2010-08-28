using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csharp_github_api.Models;
using RestSharp;
using RestSharp.Contrib;

namespace csharp_github_api.API
{
    public static class UserApiExtensions
    {
        public static IList<string> GetFollowing(this User user)
        {
            var client = new RestClient(user.Api.BaseUrl);

            var request = new RestRequest
            {
                Resource = string.Format("/user/show/{0}/following", user.Login),
                //RootElement = "user"
            };

            var response = client.Execute(request);
            var parsed = HttpUtility.ParseQueryString(response.Content);
            return parsed.GetValues(0).ToList();
        }
    }
}
