using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response.Auth
{
    public class LoginResponse
    {
        public long? Id { get; set; }
        public string? UserName { get; set; } 
        public string? FullName { get; set; }
        public string? RoleName { get; set; }
        public string? AvatarUrl { get; set; }
        public bool? isLocked { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshExpiresAt { get; set; }
    }
}
