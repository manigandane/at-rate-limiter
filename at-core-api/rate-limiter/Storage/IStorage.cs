using System;
using System.Collections.Generic;
using rate_limiter.Models;

namespace rate_limiter.Storage
{
    public interface IStorage
    {
        public bool StoreRequest(RlRequest request);
        public IEnumerable<RlRequest> GetAllRequests();
        public bool DeleteRequest(RlRequest request);
    }
}
