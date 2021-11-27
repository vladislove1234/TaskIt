using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskit_server.Model.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
