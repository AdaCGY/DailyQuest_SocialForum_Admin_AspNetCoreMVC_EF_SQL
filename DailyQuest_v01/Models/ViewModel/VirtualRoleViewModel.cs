using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models.ViewModel
{
    public class VirtualRoleViewModel
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public string? RoleDescription { get; set; }

        public IFormFile? RolePhoto { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
