using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities.TeamModels
{
    public class Team : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual List<User> Users { get
            {
                if (_users == null)
                    return new List<User>();
                return _users;
            }
            set
            {
                _users = value;
            }
        }
        private List<User> _users;
        [Required]
        public virtual List<Role> Roles
        {
            get
            {
                if (_roles == null)
                    return new List<Role>();
                return _roles;
            }
            set
            {
                _roles = value;
            }
        }
        private List<Role> _roles;
    }
}
