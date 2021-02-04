using System;
using System.Collections.Generic;
using rate_limiter.Models;

namespace rate_limiter.Storage
{
    public class MemoryStorage : IStorage
    {
        private List<RlRequest> rlRequests;
        public MemoryStorage()
        {
            rlRequests = new List<RlRequest>();
        }

        public bool DeleteRequest(RlRequest request)
        {
            var itemToDelete = rlRequests.FindIndex(item => item.Time == request.Time && item.Context.RequestIdentifier == request.Context.RequestIdentifier);

            if (itemToDelete >= 0)
            {
                rlRequests.RemoveAt(itemToDelete);
                return true;
            }

            return false;
        }

        public List<RlRequest> GetAllRequests()
        {
            return rlRequests;
        }

        public bool StoreRequest(RlRequest request)
        {
            rlRequests.Add(request);
            return true;
        }

        public bool RemoveRequestsOlderThan(DateTime olderThan)
        {
            rlRequests.RemoveAll(item => item.Time < olderThan);
            return true;
        }
    }
}
