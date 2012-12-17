using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class TestLogging
    {
        private const string GitHubUrl = @"https://api.github.com";

        [Test]
        public void Should_SeeDebugOutputInVSConsole()
        {
            var github = new Github(GitHubUrl).WithLogger(type => new DebugLogger(type));
        }
    }
}
