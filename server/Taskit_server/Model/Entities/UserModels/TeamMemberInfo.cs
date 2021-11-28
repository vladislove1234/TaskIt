using System;
using System.Collections.Generic;
using Taskit_server.Model.Entities.RoleModels;

namespace Taskit_server.Model.Entities.UserModels
{
    public class TeamMemberInfo
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public List<Role> Roles { get; set; } 
    }
}
