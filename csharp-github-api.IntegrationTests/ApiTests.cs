using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csharp_github_api.Core;
using NUnit.Framework;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class ApiTests
    {
        [Test]
        [ExpectedException(typeof(GitHubResponseException))]
        [Ignore("Fix me")]
        public void ExceptionThrownForBadRequest()
        {
            var github = new Github("http://github.com/api/v2/json");
            //github.User.GetFollowers("sgrassie");
        }
    }
}
