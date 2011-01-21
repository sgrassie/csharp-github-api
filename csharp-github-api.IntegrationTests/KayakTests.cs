using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kayak;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class KayakTests
    {
        [Test]
        public void Can_CreateServer()
        {
            var server = new DotNetServer();

            server.Host((env, respond, error) => respond(new Tuple<string, IDictionary<string, IEnumerable<string>>, IEnumerable<object>>(
                                                             "200 OK",
                                                             new Dictionary<string, IEnumerable<string>>
                                                                 {
                                                                     {"Content-Type", new[] { "text/html"}}
                                                                 },
                                                             new object[] { Encoding.ASCII.GetBytes("Hello, World!")})));
        }
    }
}
