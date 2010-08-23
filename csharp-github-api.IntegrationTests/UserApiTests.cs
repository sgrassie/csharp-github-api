using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class UserApiTests
    {
        [Test]
        public void GetUser_shouldReturnDeserialisedUserObject()
        {
            var github = new Github(@"C:\development\secretpasswordfile.xml");

            var user = github.GetUser("sgrassie");

            Assert.That(user.Name, Is.StringMatching("Stuart Grassie"));
        }
    }
}
