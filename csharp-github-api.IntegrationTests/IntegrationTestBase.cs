using System;
using Kayak;
using Kayak.Framework;

namespace csharp_github_api.IntegrationTests
{
    public abstract class IntegrationTestBase : IDisposable
    {
        protected readonly KayakServer WebServer = new KayakServer();
        protected string BaseUrl = "http://localhost:8080";

        protected IntegrationTestBase()
        {
            WebServer.UseFramework();
            WebServer.Start();
        }

        public void Dispose()
        {
            WebServer.Stop();
        }
    }
}
