using csharp_github_api.Framework;
using StructureMap;
using StructureMap.Configuration.DSL;
using RestSharp;

namespace csharp_github_api.IntegrationTests.Bootstrap
{
    public class Bootstrapper
    {
        public static void Bootstrap()
        {
            ObjectFactory.Initialize(x => x.Scan(s =>
                                                     {
                                                         s.AssemblyContainingType<IGitHubApiSettings>();
                                                         s.TheCallingAssembly();
                                                         s.LookForRegistries();
                                                         s.With(new SettingsScanner());
                                                     }));
        }
    }

    public class TestsRegistry : Registry
    {
        public TestsRegistry()
        {
            SelectConstructor(() => new Github(null));
            For<Github>().Use<Github>().Ctor<IGitHubApiSettings>().Is(x => x.GetInstance<GitHubApiSettings>());
            SelectConstructor(() => new GitHubAuthenticator(null));
            For<GitHubAuthenticator>().Use<GitHubAuthenticator>().Ctor<IGitHubApiSettings>().Is(
                x => x.GetInstance<GitHubApiSettings>());
        }
    }
}
