using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities
{
    public class Team : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
        [ForeignKey("ColumnsId")]
        public ICollection<TaskColumn> TaskColumns { get; set; }

        [ForeignKey("TasksId")]
        public ICollection<Task> Tasks { get; set; }
    }
}
