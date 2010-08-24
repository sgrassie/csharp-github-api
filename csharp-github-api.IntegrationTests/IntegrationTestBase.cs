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
                    // Purely an aid to unit testing.
                    e.Invocation.Context.Response.Headers.Add("Authenticated", "true");

                    // We don't want to write anything here because it will interfere with our json response from the Services
                    //e.Invocation.Context.Response.Write(string.Join("|", parts));
                }
            }
        }

        public void Dispose()
        {
            WebServer.Stop();
        }
    }
}
