using System;
using System.ComponentModel.DataAnnotations;

namespace Taskit_server.Model.Entities.TakenTaskModels
{
    public class TakenTask : BaseEntity
    {
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
