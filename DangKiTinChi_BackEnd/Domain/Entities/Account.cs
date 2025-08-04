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
    public class Account : EntityBase
    {
        // Tên đăng nhập của tài khoản
        [Required, StringLength(50)]
        public string? Username { get; set; }
        // Mật khẩu của tài khoản
        [Required, StringLength(50)]
        public string? Password { get; set; }
        // Họ và tên của người dùng
        [Required, StringLength(150)]
        public string? FullName { get; set; }
        // Cookie của người dùng đó
        public string? Cookie { get; set; }
        // Học kỳ hiện tại của người dùng
        [StringLength(50)]
        public string? SemeterName { get; set; }
        // Url của trường học
        public string? DomainSchool { get; set; }
        // Token của người dùng
        public string? __RequestVerificationToken { get; set; }

        public long? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
