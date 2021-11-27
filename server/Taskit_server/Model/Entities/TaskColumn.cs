using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskit_server.Model.Entities
{
    public class TaskColumn : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [ForeignKey("RolesId")]
        public ICollection<Role> Roles { get; set; }
        [ForeignKey("TasksId")]
        public ICollection<Task> Tasks { get; set; }
    }
}
