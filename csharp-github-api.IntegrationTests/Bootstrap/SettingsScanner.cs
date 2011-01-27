using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using csharp_github_api.Framework;

namespace csharp_github_api.IntegrationTests.Bootstrap
{
    /**
     * See http://www.lostechies.com/blogs/joshuaflanagan/archive/2009/07/12/how-we-handle-application-configuration.aspx
     * for more details about this. I think it's great :)
     * */

    public class SettingsScanner : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (!type.Name.EndsWith("Settings")) return;

            registry.For(type).EnrichWith(
                (session, original) =>
                    session.GetInstance<ISettingsProvider>()
                    .PopulateSettings((IGitHubApiSettings) original)
                );
        }
    }
}
