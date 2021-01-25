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
        public FixedWindowStrategy(IStorage storage): base(storage)
        {

        }

        public override RlResponse ValidateRequest(RlRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
