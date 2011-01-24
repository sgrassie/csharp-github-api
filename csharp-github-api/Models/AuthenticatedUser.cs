using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csharp_github_api.Core;

namespace csharp_github_api.Models
{
    public class AuthenticatedUser
    {
        internal UserApi _userApi;

        internal AuthenticatedUser(){ }

        /// <summary>
        /// Follow the specified user. Must be authenticated.
        /// </summary>
        /// <param name="username">The user name of the user on github to follow.</param>
        /// <returns></returns>
        public bool Follow(string username)
        {
            return _userApi.Authenticated().Follow(username);
        }

        public bool UnFollow(string username)
        {
            return _userApi.Authenticated().UnFollow(username);
        }
    }
}
