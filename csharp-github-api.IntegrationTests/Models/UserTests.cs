using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StructureMap;
using FluentAssertions;
using RestSharp;

namespace csharp_github_api.IntegrationTests.Models
{
    [TestFixture]
    public class UserTests
    {
        private Github _github;

        [TestFixtureSetUp]
        public void Setup()
        {
            _github = new Github();
        }

        
    }
}
