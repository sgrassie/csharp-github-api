//-----------------------------------------------------------------------
// <copyright file="RepositoriesCollaborators.cs" company="TemporalCohesion.co.uk">
//     Copyright 2012 - Present Stuart Grassie
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

using RestSharp;
namespace GitHubAPI.Api.Repositories
{
    public static class RepositoriesCollaborators
    {

        //http://developer.github.com/v3/repos/collaborators/#add-collaborator
        //Strange one, has no response object
        public static IRestResponse AddCollaborator(this GithubRestApiClient client,
  string RepoOwner, string Repo, string UserID)
        {
           var request = client.RequestFactory.CreateRequest(
                () =>
                {
                    var req = new RestRequest("/repos/{owner}/{repo}/collaborators/{user}")
                    {
                        Method = Method.PUT,
                        RequestFormat = DataFormat.Json
                    };

                    req.AddUrlSegment("owner", RepoOwner);
                    req.AddUrlSegment("repo", Repo);
                    req.AddUrlSegment("user", UserID);
                    return req;
                });

            var response = client.Execute(request);

            return response;
        }
    }
}
