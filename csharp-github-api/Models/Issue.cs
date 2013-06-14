//-----------------------------------------------------------------------
// <copyright file="Issue.cs" company="TemporalCohesion.co.uk">
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

namespace GitHubAPI.Models
{
    /// <summary>
    /// Maps to an issue
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Gets or sets the title of the GitHub issue.
        /// </summary>
        public virtual string Title { get; set;}

        /// <summary>
        /// Gets or sets the issue number.
        /// </summary>
        public virtual long Number { get; set;}


        /// <summary>
        /// Gets or sets the issue user (maybe who created it?).
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the issue user (maybe who created it?).
        /// </summary>
        public virtual User Assignee { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is Issue)
            {
                var compareTo = (Issue) obj;

                if (compareTo.Title.Equals(Title) && compareTo.Number.Equals(Number)
                   )
                {
                    return true;
                }

                return false;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode() + Number.GetHashCode();
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
