using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities
{
    public class Task : BaseEntity
    {
        [Required]
        public DateTime? Deadline { get; set; }
        [Required]
        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        public ICollection<Role> Roles { get; set; }
        [Required]
        public string Content { get; set; }
        [ForeignKey("ColumnId")]
        public TaskColumn? Column { get; set; }

        public TaskState? State{ get; set; }
        [ForeignKey("UsersId")]
        public ICollection<User> Users { get; set; }
    }
    public enum TaskState
    {
        InProgress,
        Idle,
        Done
    }
}
