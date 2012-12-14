//-----------------------------------------------------------------------
// <copyright file="Plan.cs" company="TemporalCohesion.co.uk">
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
    /// <summary>
    /// Maps to an authenticated users plan
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// Gets or sets the name of the GitHub plan.
        /// </summary>
        /// <remarks>
        /// One of: Free, Micro, Small, Medium
        /// </remarks>
        public virtual string Name { get; set;}

        /// <summary>
        /// Gets or sets the number of collaborators in the private plan
        /// </summary>
        public virtual int Collaborators { get; set;}

        /// <summary>
        /// Gets or sets the amount of disk space in the private plan.
        /// </summary>
        public virtual long Space { get; set;}

        /// <summary>
        /// Gets or sets the number of private repositories the user has.
        /// </summary>
        public virtual int PrivateRepos { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Plan)
            {
                var compareTo = (Plan) obj;

                if (compareTo.Equals(Name) && compareTo.Collaborators.Equals(Collaborators)
                    && compareTo.Space.Equals(Space) && compareTo.PrivateRepos.Equals(PrivateRepos))
                {
                    return true;
                }

                return false;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Collaborators.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
