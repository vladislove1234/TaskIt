using System;
using System.Collections.Generic;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities.TeamModels
{
    public class TeamInfo
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public List<Role> Roles { get; set; }
        public List<TeamMemberInfo> Members { get; set; }
    }
}
