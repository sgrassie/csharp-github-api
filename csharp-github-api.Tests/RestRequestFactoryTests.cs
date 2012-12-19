using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

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

    public class RestRequestFactory
    {
        private RestRequestFactory()
        {
        }

        public static IRestRequest CreateRequest(Func<IRestRequest> request)
        {
            return request.Invoke();
        }

        public static IRestRequest CreateRequest(string resource, Method method = Method.GET, params Parameter[] parameters)
        {
            return CreateRequest(
                () =>
                    {
                        var request = new RestRequest
                            {
                                Resource = resource,
                                Method = method
                            };

                        if (parameters.Length == 0) return request;

                        foreach (var parameter in parameters)
                        {
                            request.AddParameter(parameter);
                        }

                        return request;
                    }
                );
        }

        public static IRestRequest CreateRequest(string resource, Method method = Method.GET, params KeyValuePair<string, string>[] parameters)
        {
            return CreateRequest(
                () =>
                    {
                        var request = new RestRequest
                                          {
                                              Resource = resource,
                                              Method = method
                                          };

                        if (parameters.Length == 0) return request;

                        foreach (var kvp in parameters)
                        {
                            request.AddUrlSegment(kvp.Key, kvp.Value);
                        }

                        return request;
                    }
                );
        }
    }
}
