using System;
using System.Collections.Generic;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
