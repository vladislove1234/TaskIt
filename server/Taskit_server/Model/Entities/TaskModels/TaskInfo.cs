using System;
using System.Collections.Generic;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities.TaskModels
{
    public class TaskInfo
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int TaskId { get; set; }
        public int Price { get; set; }
        public List<TeamMemberInfo> Performers { get; set; }
        public List<Role> Roles { get; set; }
        public DateTime Deadline { get; set; }
        public TaskState State { get; set; }
        public TaskInfo(Task task,List<TeamMemberInfo> members)
        {
            Name = task.Name;
            Content = task.Content;
            TaskId = task.Id;
            Price = task.Price;
            Performers = members;
            Roles = task.Roles;
            Deadline = task.Deadline;
            State = task.State;
        }
    }
}
