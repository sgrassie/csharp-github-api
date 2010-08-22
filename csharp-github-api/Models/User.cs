using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp_github_api.Models
{
    /// <summary>
    /// Represents a GitHub.com user account.
    /// </summary>
    public class User
    {
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

    }
}
