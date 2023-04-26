﻿using DataLayer.Enums;

namespace DataLayer.Entities
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public int StudentId { get; set; }
    }
}