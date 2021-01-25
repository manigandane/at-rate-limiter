using System;
namespace at_core_api.Models
{
    public class AirTask
    {
        public string Tasker { get; set; }
        public string Requester { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime RequestTime { get; set; }
        public string Description { get; set; }
    }
}
