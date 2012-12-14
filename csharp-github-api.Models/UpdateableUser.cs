//-----------------------------------------------------------------------
// <copyright file="UpdateableUser.cs" company="TemporalCohesion.co.uk">
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

namespace csharp_github_api.Models
{
    public class UpdateableUser
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Blog { get; set; }
        public virtual string Company { get; set; }
        public virtual string Location { get; set; }
        public virtual bool Hireable { get; set; }
        public virtual string Bio { get; set; }
    }
}
