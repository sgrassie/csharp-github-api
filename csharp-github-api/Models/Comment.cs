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
    /// Maps to a comment
    /// </summary>
    public class IssueComment
    {
        /*
        {
          "id": 1,
          "url": "https://api.github.com/repos/octocat/Hello-World/issues/comments/1",
          "html_url": "https://github.com/octocat/Hello-World/issues/1347#issuecomment-1",
          "body": "Me too",
          "user": {
            "login": "octocat",
            "id": 1,
            "avatar_url": "https://github.com/images/error/octocat_happy.gif",
            "gravatar_id": "somehexcode",
            "url": "https://api.github.com/users/octocat"
          },
          "created_at": "2011-04-14T16:00:49Z",
          "updated_at": "2011-04-14T16:00:49Z"
        }
         */
        /// <summary>
        /// Gets or sets the text of the GitHub issue comment.
        /// </summary>
        public virtual string Body { get; set;}

        /// <summary>
        /// Gets or sets the comment Id.
        /// </summary>
        public virtual long Id { get; set;}


        /// <summary>
        /// Gets or sets the URL of the comment.
        /// </summary>
        public virtual string Url { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is IssueComment)
            {
                var compareTo = (IssueComment)obj;

                if (compareTo.Id.Equals(Id) && compareTo.Url.Equals(Url)
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
            return Url.GetHashCode() + Id.GetHashCode();
        }

        public override string ToString()
        {
            return Body;
        }
    }
}
