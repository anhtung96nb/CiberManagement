﻿using Microsoft.AspNetCore.Identity;

namespace CiberManagement.DAL.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string Address { get; set; }
    }
}
