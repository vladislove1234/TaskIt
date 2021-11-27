using System;
using System.ComponentModel.DataAnnotations;

namespace Taskit_server.Model.Entities.TeamModels
{
    public class TeamAddRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
