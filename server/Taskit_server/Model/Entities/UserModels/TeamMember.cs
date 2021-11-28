using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.TaskModels;

namespace Taskit_server.Model.Entities.UserModels
{
    public class TeamMember : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        
        public virtual List<Role> Roles
        {
            get
            {
                if (_roles == null)
                    _roles = new List<Role>();
                return _roles;
            }
            set
            {
                _roles = value;
            }
        }
        private List<Role> _roles;
        
        public virtual List<Task> Tasks
        {
            get
            {
                if (_tasks == null)
                    _tasks = new List<Task>();
                return _tasks;
            }
            set
            {
                _tasks = value;
            }

        }
        private List<Task> _tasks;
    }
}
