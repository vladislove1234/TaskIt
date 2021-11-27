using System;
using System.Collections.Generic;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities
{
    public class Task : BaseEntity
    {
        public DateTime? Deadline { get; set; }
        public User Author { get; set; }
        public ICollection<Role> Roles { get; set; }
        public string Content { get; set; }
        public TaskColumn? Column { get; set; }
        public TaskState? State{ get; set; }
        public ICollection<User> Users { get; set; }
    }
    public enum TaskState
    {
        InProgress,
        Idle,
        Done
    }
}
