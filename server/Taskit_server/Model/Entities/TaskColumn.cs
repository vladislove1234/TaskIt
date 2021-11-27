using System;
using System.Collections;
using System.Collections.Generic;

namespace Taskit_server.Model.Entities
{
    public class TaskColumn : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
