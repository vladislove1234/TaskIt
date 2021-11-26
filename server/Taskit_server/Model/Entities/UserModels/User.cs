﻿using System;
using System.Collections.Generic;

namespace Taskit_server.Model.Entities.UserModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<User> Friends { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
