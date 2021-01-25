using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using at_core_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace at_core_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirTaskController : ControllerBase
    {
        private static readonly AirTask[] Tasks = new AirTask[]
        {
           new AirTask {Tasker = "Ozil", Description="Cleanup the football mess", IsCompleted = false, Requester="Arteta", RequestTime = DateTime.UtcNow},
           new AirTask {Tasker = "Xhaka", Description="Defend the goal advantage", IsCompleted = false, Requester="Partey", RequestTime = DateTime.UtcNow},
           new AirTask {Tasker = "John", Description="Prepare the ground for training", IsCompleted = true, Requester="Edu", RequestTime = DateTime.UtcNow.AddDays(-1)}
        };

        [HttpGet]
        public IEnumerable<AirTask> Get()
        {
            return Tasks;
        }
    }
}
