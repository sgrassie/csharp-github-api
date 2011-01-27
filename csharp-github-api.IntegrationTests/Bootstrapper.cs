using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csharp_github_api.Framework;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace csharp_github_api.IntegrationTests
{
    public class Bootstrapper
    {
        public static void Bootstrap()
        {
            ObjectFactory.Initialize(x=>x.Scan(s=>
                                                  {
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
            SelectConstructor(()=> new Github(null as IGitHubApiSettings));
            For<Github>().Use<Github>().Ctor<IGitHubApiSettings>().Is(x => x.GetInstance<GitHubApiSettings>());
        }
    }
}
