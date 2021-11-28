using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Taskit_server.Model.Entities.TeamModels
{
    public class TeamAddRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual List<int> UsersId { get; set; }
    }
}
