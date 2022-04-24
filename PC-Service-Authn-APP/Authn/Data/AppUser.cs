﻿using System.ComponentModel.DataAnnotations;

namespace Authn.Data
{
    public class AppUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Provider { get; set; }
        public string NameIdentifier { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Roles { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<string> RoleList
        {
            get
            {
                return Roles.Split(',').ToList();
            }
        }
    }
}
