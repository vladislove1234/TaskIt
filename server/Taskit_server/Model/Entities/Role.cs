using System;
namespace Taskit_server.Model.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public string Color { get; set; }
        public Team Team { get; set; }
    }
}
