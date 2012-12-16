//-----------------------------------------------------------------------
// <copyright file="GitHubResponseException.cs" company="TemporalCohesion.co.uk">
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

namespace csharp_github_api.Exceptions
{
    [Serializable]
    public class GitHubResponseException : Exception
    {
        public GitHubResponseException() { }
        public GitHubResponseException(string message) : base(message) { }
        public GitHubResponseException(string message, Exception inner) : base(message, inner) { }
        protected GitHubResponseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Gets or sets the raw error response from GitHub.
        /// </summary>
        public object Response
        {
            get; set;
        }
    }
}
