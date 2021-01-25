using System;
namespace rate_limiter.Models
{
    public class RlRequest
    {
        private RequestContext _context;
        private DateTime _time;
        public RlRequest(RequestContext requestContext, DateTime time)
        {
            this._context = requestContext;
            this._time = time;
        }
        public RequestContext Context => this._context;
        public DateTime Time => this._time;
    }
}
