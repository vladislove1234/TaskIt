using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taskit_server.Model.Entities.TakenTaskModels;
using Taskit_server.Model.Entities.TeamModels;

namespace Taskit_server.Model.Entities.UserModels
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [ForeignKey("FirendsId")]
        public virtual List<User> Friends { get; set; }

        public virtual List<TakenTask> TaskenTasks { get; set; }

        public virtual List<Team> Teams { get; set; }
    }
}
