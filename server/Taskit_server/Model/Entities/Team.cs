﻿using System;
using System.Collections.Generic;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<TaskColumn> TaskColumns { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
