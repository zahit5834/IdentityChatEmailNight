﻿using Microsoft.AspNetCore.Identity;

namespace IdentityChatEmailNight.Entites
{
    public class AppUser:IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? City { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
