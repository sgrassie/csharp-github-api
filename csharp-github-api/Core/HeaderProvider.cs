//-----------------------------------------------------------------------
// <copyright file="HeaderProvider.cs" company="TemporalCohesion.co.uk">
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

namespace GitHubAPI.Core
{
    using System;
    using System.Collections.Generic;
    using RestSharp;

    public interface IHeaderProvider
    {
        HashSet<IHeader> Headers { get; set; }
        void AddHeader(IHeader header);
        void PopulateHeaders(ref IRestRequest request);
    }

    public class HeaderProvider : IHeaderProvider
    {
        private HashSet<IHeader> _headers = new HashSet<IHeader>();

        public HashSet<IHeader> Headers
        {
            get { return _headers; }
            set { _headers = value; }
        }
        
        public void AddHeader(IHeader header)
        {
            _headers.Add(header);
        }

        public void PopulateHeaders(ref IRestRequest request)
        {
            if(request == null) throw new ArgumentNullException("request", "The request must not be null!");

            foreach (var header in Headers)
            {
                request.AddHeader(header.Name, header.Value);
            }
        }
    }
}