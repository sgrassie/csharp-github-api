using System;
using System.Collections.Generic;
using RestSharp;

namespace csharp_github_api.Core
{
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