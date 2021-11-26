using System;
using System.ComponentModel.DataAnnotations;

namespace Taskit_server.Model.Entities.UserModels
{
    public class UserAuthentificationRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
