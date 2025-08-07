using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : EntityBase
    {
        // Tên đăng nhập
        [Required, StringLength(50)]
        public string? UserName { get; set; }
        // Mật khẩu
        [Required, StringLength(50)]
        public string? Password { get; set; }
        // Họ và tên
        [Required, StringLength(50)]
        public string? FullName { get; set; }
        // Số dư tài khoản
        public long? Money { get; set; }
        // Ảnh đại diện
        public string? AvatarUrl { get; set; }
        // Trạng thái khoá của người dùng
        public bool? IsLocker { get; set; } = false;
        // Role Infomation
        public long? RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Role? Role { get; set; }


        public ICollection<TokenUser>? TokenUsers { get; set; }
        public ICollection<HistoryMoney>? HistoryMoneys { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}
