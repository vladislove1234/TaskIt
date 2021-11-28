using System;
using Taskit_server.Model.Entities.TaskModels;

namespace Taskit_server.Model.Entities.TakenTaskModels
{
    public class TakenTaskInfo
    {
        public TaskInfo Task { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Color { get; set; }
    }
}
