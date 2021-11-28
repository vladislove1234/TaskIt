using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.TeamModels;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities.TaskModels
{
    public class Task : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public virtual Team Team { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public TaskState State { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
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
        [Required]
        public virtual List<TeamMember> Performers
        {
            get
            {
                if (_performers == null)
                    _performers = new List<TeamMember>();
                return _performers;
            }
            set
            {
                _performers = value;
            }
        }
        private List<TeamMember> _performers;
    }
    public enum TaskState
    {
        Todo,
        InProgress,
        Done
    }
}
