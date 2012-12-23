using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LoggingExtensions.log4net;
using NUnit.Framework;
using csharp_github_api.IntegrationTests.Properties;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class TestLogging
    {
        private const string GitHubUrl = @"https://api.github.com";

        [Test]
        public void Should_SeeDebugOutputInVSConsole()
        {
            using(var stream = new MemoryStream(TestResources.log4net))
            log4net.Config.XmlConfigurator.Configure(stream);

            var github = new Github(GitHubUrl).WithLogger<Log4NetLog>();
        }
    }
}
