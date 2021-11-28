using System;
using System.ComponentModel.DataAnnotations;

namespace Taskit_server.Model.Entities.RoleModels
{
    public class AddRoleRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
