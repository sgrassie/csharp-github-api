using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Text;
using csharp_github_api.Core;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class HeaderProviderTests
    {
        [Test]
        [Ignore("Not sure what default headers to provide yet.")]
        public void should_provide_default_headers()
        {
            var provider = new HeaderProvider();

            var headers = provider.Headers;

            headers.Should().NotBeEmpty("Default headers should be provided.");
        }

        [Test]
        public void addheader_should_add_to_default_headers()
        {
            var provider = new HeaderProvider();
            var header = new Header("name", "value");
            provider.AddHeader(header);

            provider.Headers.Contains(header).Should().BeTrue();
        }
    }
}
