#if NUNIT
using NUnit.Framework;
#else
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Fact = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using TestFixtureSetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using SetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using Is = NUnit.Framework.Is;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;
using GitHubAPI.Extensions;

namespace GitHubAPI.Tests
{
    [TestFixture]
    public class MiscTests
    {
        [Test]
        public void DynamicResponse_WithCustomDeserializer()
        {
            var restRequest = new RestRequest
            {
                Resource = "/users/sgrassie"
            };

            var client = new RestClient
            {
                BaseUrl = "https://api.github.com"
            };
            client.AddHandler("application/json", new DynamicJsonDeserializer());

            dynamic response = client.Execute<dynamic>(restRequest);

            NUnit.Framework.Assert.That(response.Data["login"], Is.StringMatching("sgrassie"));
        }

        [Test]
        public void DynamicReponse_WithExtensionMethod()
        {
            var restRequest = new RestRequest
            {
                Resource = "/users/sgrassie"
            };

            var client = new RestClient
            {
                BaseUrl = "https://api.github.com"
            };

            var response = client.Execute(restRequest);
            var dynamicPart = response.Dynamic();

            NUnit.Framework.Assert.That(dynamicPart.login, Is.StringMatching("sgrassie"));
        }
    }

    public class DynamicJsonDeserializer : IDeserializer
    {
        public string RootElement { get; set; }

        public string Namespace { get; set; }

        public string DateFormat { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            return (T)SimpleJson.DeserializeObject(response.Content);
        }
    }
}
