//-----------------------------------------------------------------------
// <copyright file="AuthenticatedUser.cs" company="TemporalCohesion.co.uk">
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

using System.Collections.Generic;

namespace csharp_github_api.Models
{
    using Core;
    using System;

    /// <summary>
    /// Allows access to actions which the user must be authenticated for.
    /// </summary>
    /// <remarks>
    /// If the user is not authenticatd, then the actions will fail.
    /// </remarks>
    [Obsolete("Not used. Redesigned.")]
    public class AuthenticatedUser
    {
        //private readonly UserApi _userApi;

        //public AuthenticatedUser(UserApi userApi )
        //{
        //    _userApi = userApi;
        //}

        ///// <summary>
        ///// Follow the specified user. Must be authenticated.
        ///// </summary>
        ///// <param name="username">The user name of the user on github to follow.</param>
        ///// TODO: Consider returning the follow list from the response
        ///// <returns><c>True</c> if the user was followed; otherwise <c>false</c>.</returns>
        //public bool Follow(string username)
        //{
        //    return _userApi.Authenticated().Follow(username);
        //}

        ///// <summary>
        ///// Unfollow the specified user. Must be authenticated.
        ///// </summary>
        ///// <param name="username">The user name of the user on github to follow.</param>
        ///// TODO: Consider returning the follow list from the response
        ///// <returns><c>True</c> if the user was followed; otherwise <c>false</c>.</returns>
        //public bool UnFollow(string username)
        //{
        //    return _userApi.Authenticated().UnFollow(username);
        //}

        //public bool Update(UpdateableUser user)
        //{
        //    return true;
        //}
    }
}
