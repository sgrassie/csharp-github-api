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
        protected string BaseUrl = "http://localhost:8080";
        protected int RequestCount;

        protected IntegrationTestBase()
        {
            var framework = WebServer.UseFramework();
            framework.Invoking += framework_Invoking;
            WebServer.Start();

        }

        void framework_Invoking(object sender, InvokingEventArgs e)
        {
            RequestCount++;

            var header = e.Invocation.Context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(header))
            {
                var parts = Encoding.ASCII.GetString(Convert.FromBase64String(header.Substring("Basic ".Length))).Split(':');

                if (parts.Count() == 2)
                {
                    /* On some requests, if you are authenticated, you get extra information back in the response,
                     * or are allowed to modify/create things,so with this we can simulate that in the Services */
                    e.Invocation.Context.Response.Headers.Add("Authenticated", "true");
                }
            }
        }

        public void Dispose()
        {
            WebServer.Stop();
        }
    }
}
