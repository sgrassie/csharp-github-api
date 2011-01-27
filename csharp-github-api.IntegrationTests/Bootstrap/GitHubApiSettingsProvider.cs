//-----------------------------------------------------------------------
// <copyright file="GitHubApiSettingsProvider.cs" company="TemporalCohesion.co.uk">
//     Copyright [2010] [Stuart Grassie]
//
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
//----------------------------------------------------------------------

using System;
using csharp_github_api.Framework;
using System.IO;
using RestSharp;
using RestSharp.Deserializers;

namespace csharp_github_api.IntegrationTests.Bootstrap
{
    /// <summary>
    /// The default settings provider for the API.
    /// </summary>
    public class GitHubApiSettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// Populates the settings object.
        /// </summary>
        /// <param name="settings">The <see cref="IGitHubApiSettings"/> object to pass to the API.</param>
        /// <returns>The populated <paramref name="settings"/> object.</returns>
        /// <exception cref="ArgumentNullException" />
        public IGitHubApiSettings PopulateSettings(IGitHubApiSettings settings)
        {
            if(settings == null) throw new ArgumentNullException("settings", "The settings object cannot be null");

            var secretsJson = File.ReadAllText("secrets.json");
            var json = new JsonDeserializer();
            var secrets = json.Deserialize<Secrets>(new RestResponse {Content = secretsJson});

            settings.Username = secrets.Username;
            settings.Password = secrets.Password;
            settings.Token = secrets.Token;

            if (!string.IsNullOrEmpty(secrets.BaseUrl))
                settings.BaseUrl = secrets.BaseUrl;

            return settings;
        }
    }

    public class Secrets
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string BaseUrl { get; set; }
    }
}
