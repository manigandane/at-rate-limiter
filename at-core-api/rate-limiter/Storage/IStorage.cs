using System;
using System.Collections.Generic;
using rate_limiter.Models;

namespace rate_limiter.Storage
{
    public interface IStorage
    {
        public bool StoreRequest(RlRequest request);
        public List<RlRequest> GetAllRequests();
        public bool DeleteRequest(RlRequest request);
    }
}
