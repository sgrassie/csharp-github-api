//-----------------------------------------------------------------------
// <copyright file="ActivityEvents.cs" company="TemporalCohesion.co.uk">
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

namespace csharp_github_api.Api.Activity
{
    using RestSharp;

    /// <summary>
    /// <para> This is a read-only API to the GitHub events. These events power the various activity streams on the site.</para>
    /// </summary>
    /// <see cref="http://developer.github.com/v3/activity/events/"/>
    public static class ActivityEvents
    {
       public static IRestResponse Events(GithubRestApiClient client)
       {
           return null;
       }
    }
}
