using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Taskit_server.Model.Entities.TeamModels;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Model.Entities.TaskModels
{
    public class TaskAddRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public List<int> PerformersId { get; set; }
        [Required]
        public List<int> RolesId { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
    }
}
