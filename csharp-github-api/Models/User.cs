//-----------------------------------------------------------------------
// <copyright file="User.cs" company="TemporalCohesion.co.uk">
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

using csharp_github_api.Core;
using System.Collections.Generic;
namespace csharp_github_api.Models
{
    /// <summary>
    /// Represents a GitHub.com user account.
    /// </summary>
    public class User
    {
        internal UserApi UserApi;

        /* public, authentication not required */
        public virtual int Id { get; set; }
        public virtual string Login { get; set; }
        public virtual string Name { get; set;}
        public virtual string Company { get; set;}
        public virtual string Location { get; set;}
        public virtual string Email { get; set;}
        public virtual string Blog { get; set;}
        public virtual int FollowingCount { get; set;}
        public virtual int FollowersCount { get; set;}
        public virtual int PublicGistCount { get; set;}
        public virtual int PublicRepoCount { get; set;}

        /*private, authentication required */
        public virtual int TotalPrivateRepoCount { get; set;}
        public virtual int Collaborators { get; set;}
        public virtual long DiskUsage { get; set;}
        public virtual int OwnedPrivateRepoCount { get; set;}
        public virtual int PrivateGistCount { get; set;}
        public virtual Plan Plan { get; set;}

        public User Authenticated
        {
            get
            {
                return this;
            }
        }

        public IEnumerable<string> Following
        {
            get
            {
                return UserApi.GetFollowing(this);
            }
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of the usernames of the followers of this user.
        /// </summary>
        public IEnumerable<string> Followers
        {
            get
            {
                return UserApi.GetFollowers(this);
            }
        }

        /// <summary>
        /// Follow the specified user. Must be authenticated.
        /// </summary>
        /// <param name="username">The user name of the user on github to follow.</param>
        /// <returns></returns>
        public bool Follow(string username)
        {
            return UserApi.Authenticated().Follow(username);
        }

        public override bool Equals(object obj)
        {
            if(obj is User)
            {
                var compareTo = (User) obj;

                return compareTo.Id.Equals(Id) && compareTo.Name.Equals(Name) && compareTo.Email.Equals(Email);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + Login.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
