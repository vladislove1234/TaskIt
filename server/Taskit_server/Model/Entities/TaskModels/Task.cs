using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities.TaskModels
{
    public class Task : BaseEntity
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public virtual User Author { get; set; }
        [Required]
        public virtual List<User> Performers { get; set; }
    }
}
