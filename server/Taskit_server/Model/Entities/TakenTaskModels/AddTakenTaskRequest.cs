using System;
using System.ComponentModel.DataAnnotations;

namespace Taskit_server.Model.Entities.TakenTaskModels
{
    public class AddTakenTaskRequest
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int TaskId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
