using System;
using System.ComponentModel.DataAnnotations;

namespace Taskit_server.Model.Entities.UserModels
{
    public class UserInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
