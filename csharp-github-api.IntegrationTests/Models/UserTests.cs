using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace csharp_github_api.IntegrationTests.Models
{
    [TestFixture]
    public class UserTests
    {
        private Github _github;

        [TestFixtureSetUp]
        public void Setup()
        {
            _github = new Github("http://github.com/api/v2/json", @"C:\development\secretpasswordfile.xml");    
        }

        [Test]
        public void Should_Get_List_Of_Following()
        {
            var result = _github.User.GetUser("sgrassie").Following;

            Assert.That(result, Has.Count.GreaterThan(0));
        }

        [Test]
        public void Should_Get_List_Of_Followers()
        {
            var result = _github.User.GetUser("sgrassie").Followers;

            Assert.That(result, Has.Count.GreaterThan(0));
        }
    }
}
