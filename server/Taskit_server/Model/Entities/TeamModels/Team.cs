﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.TaskModels;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities.TeamModels
{
    public class Team : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual List<TeamMember> TeamMembers { get
            {
                if (_teamMembers == null)
                    _teamMembers = new List<TeamMember>();
                return _teamMembers;
            }
            set
            {
                _teamMembers = value;
            }
        }
        private List<TeamMember> _teamMembers;
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
