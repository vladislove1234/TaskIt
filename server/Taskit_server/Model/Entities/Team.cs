using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities
{
    public class Team : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public ICollection<User> Users { get; set; }
    }
}
