#if NUNIT
using NUnit.Framework;
#else
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Fact = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using TestFixtureSetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using SetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

using FluentAssertions;
using RestSharp;
using System;
using System.Linq;
using System.Text;
using GitHubAPI.Core;

namespace GitHubAPI.IntegrationTests
{
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

        [TestFixture]
        public class when_new_object_instance_instantiated : HeaderProviderTestsBase
        {
            public override void Because()
            {
            }

            [Fact]
            //VSTest Ignore doesn't allow comments..maybe we could derive one that does and have it in scope?
            [Ignore] //"Not sure what default headers to provide yet."
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
