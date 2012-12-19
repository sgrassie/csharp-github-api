using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using csharp_github_api.Resource;

namespace csharp_github_api.Tests
{
    [TestFixture]
    public class RestRequestFactoryTests
    {
        private RestClient _client;

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
            var request = RestRequestFactory.CreateRequest(
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
            var request = RestRequestFactory.CreateRequest("/users/{user}", Method.GET, new[]
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
            var request = RestRequestFactory.CreateRequest(
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
