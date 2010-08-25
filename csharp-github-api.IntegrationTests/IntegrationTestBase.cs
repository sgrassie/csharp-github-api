using System;
using System.Linq;
using System.Text;
using Kayak;
using Kayak.Framework;

namespace csharp_github_api.IntegrationTests
{
    public abstract class IntegrationTestBase : IDisposable
    {
        protected readonly KayakServer WebServer = new KayakServer();
        protected readonly int GitHubApiRequestLimit = 60;
        protected string BaseUrl = "http://localhost:8080";
        protected int RequestCount;
        private DateTime _timeOfPreviousRequest;
        private DateTime _timeOfCurrentRequest;

        protected IntegrationTestBase()
        {
            var framework = WebServer.UseFramework();
            framework.Invoking += framework_Invoking;
            WebServer.Start();

        }

        void framework_Invoking(object sender, InvokingEventArgs e)
        {
            RequestCount++;
            _timeOfCurrentRequest = DateTime.Now;

            if (!HasExceededApiLimit())
            {
                var header = e.Invocation.Context.Request.Headers["Authorization"];

                if (!string.IsNullOrEmpty(header))
                {
                    var parts =
                        Encoding.ASCII.GetString(Convert.FromBase64String(header.Substring("Basic ".Length))).Split(':');

                    if (parts.Count() == 2)
                    {
                        /* On some requests, if you are authenticated, you get extra information back in the response,
                         * or are allowed to modify/create things,so with this we can simulate that in the Services */
                        e.Invocation.Context.Response.Headers.Add("Authenticated", "true");
                    }
                }
            }
            else
            {
                e.Invocation.Context.Response.SetStatusToForbidden(); // Github.com would actually return access denied
            }
        }

        /// <summary>
        /// Determines whether or not the API request limit has been exceeded.
        /// </summary>
        /// <returns><c>True</c> if the API limit has been exceeded; otherwise returns <c>false</c>.</returns>
        protected virtual bool HasExceededApiLimit()
        {
            var timeSpan = _timeOfCurrentRequest.Subtract(_timeOfPreviousRequest);

            if ((timeSpan.TotalMinutes < 1.0d) && (RequestCount >= GitHubApiRequestLimit))
            {
                _timeOfPreviousRequest = _timeOfCurrentRequest;

                return true;
            }

            _timeOfPreviousRequest = _timeOfCurrentRequest;

            return false;
        }

        public void Dispose()
        {
            WebServer.Stop();
        }
    }
}
