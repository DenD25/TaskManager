﻿using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.Enums;

namespace TaskManagerAPI.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ProjectUserRolesEnum? ProjectRole { get; set; }
        public ICollection<Role> Roles { get; set; } 
        public ICollection<TaskModel>? Tasks { get; set; }
        public ICollection<ProjectUser>? Projects { get; set; }
    }
}