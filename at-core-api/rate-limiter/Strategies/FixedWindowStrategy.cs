using System;
using rate_limiter.Storage;
using rate_limiter.Models;

namespace rate_limiter.Strategies
{
    /**
     * This strategy works based on time interval.
     * Example - Allow only a specified number of calls in last x minutes
     */
    public class FixedWindowStrategy : StrategyBase
    {
        private int _maximumRequests;
        private int _windowMinutes;
        public FixedWindowStrategy(IStorage storage, int maximumRequests, int windowMinutes): base(storage)
        {
            this._maximumRequests = maximumRequests;
            this._windowMinutes = windowMinutes;
        }

        public override RlResponse ValidateRequest(RlRequest request)
        {
            var result = new RlResponse();
            //base.storage
            //check if new request is allowed
            var allReqs = base.storage.GetAllRequests();
            var currentReqs = allReqs.FindAll(item => item.Time > DateTime.UtcNow.AddMinutes(-1 * _windowMinutes));
            if (currentReqs.Count < _maximumRequests)
            {
                //allowed
                //if so, add to current list
                base.storage.StoreRequest(request);
                result.Allowed = true;
            }
            else
            {
                //reject
                result.Allowed = false;
                result.Message = "Too many requests, please try after sometime";
            }

            //send RlResponse
            return result;
        }
    }
}
