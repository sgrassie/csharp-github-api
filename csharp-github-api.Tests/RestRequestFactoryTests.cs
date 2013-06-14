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

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FluentAssertions;
using RestSharp;
using GitHubAPI.Resource;

namespace GitHubAPI.Tests
{
    [TestFixture]
    public class RestRequestFactoryTests
    {
        private RestClient _client;
        private IRestRequestFactory requestFactory = new RestRequestFactory();

        [TestFixtureSetUp]
        public void Setup()
        {
            _client = new RestClient
            {
                BaseUrl = "https://api.github.com"
            };
        }

        [Test]
        public void BasicRequestWithParameterExample()
        {
            var restRequest = new RestRequest
            {
                Resource = "/users/{user}",
                Method = Method.GET,
                
            };
            restRequest.AddUrlSegment("user", "sgrassie");

            var response = _client.Execute(restRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void CreateRequestUsingFunc()
        {
            var request = requestFactory.CreateRequest(
                () => new RestRequest
                            {
                                Resource = "/users/sgrassie",
                                Method = Method.GET
                            });

            request.Should().NotBeNull();

            var response = _client.Execute(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void CreateRequestSpecifyingResourceAndParameters()
        {
            var request = requestFactory.CreateRequest("/users/{user}", Method.GET, new[]
                                                                                            {
                                                                                                new Parameter
                                                                                                    {
                                                                                                        Name = "user",
                                                                                                        Value =
                                                                                                            "sgrassie",
                                                                                                        Type = ParameterType.UrlSegment
                                                                                                    }
                                                                                            });
            var response = _client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void CreateRequestSpecifyingResourceAndParametersWithKeyValuePairs()
        {
            var request = requestFactory.CreateRequest(
                "/users/{user}", 
                Method.GET, 
                new[]
                {
                    new KeyValuePair<string, string>("user", "sgrassie") 
                });
            var response = _client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
