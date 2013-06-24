using GitHubAPI.IntegrationTests.Ext;

namespace GitHubAPI.IntegrationTests
{
    using NUnit.Framework;
    using FluentAssertions;
    using GitHubAPI.Core;

    public class HeaderProviderTests
    {
        public abstract class HeaderProviderTestsBase : GitHubAPI.IntegrationTests.Ext.TinySpec
        {
            protected HeaderProvider HeaderProvider;

            public override void Context()
            {
                HeaderProvider = new HeaderProvider();
            }
        }

        public class when_new_object_instance_instantiated : HeaderProviderTestsBase
        {
            public override void Because()
            {
            }

            [Fact]
            [Ignore("Not sure what default headers to provide yet.")]
            public void should_provide_default_headers()
            {
                var headers = HeaderProvider.Headers;

                headers.Should().NotBeEmpty("Default headers should be provided.");
            }

            [Fact]
            public void should_be_able_to_add_to_default_headers()
            {
                var header = new Header("name", "value");
                HeaderProvider.AddHeader(header);

                HeaderProvider.Headers.Contains(header).Should().BeTrue();
            }
        }
    }
}
