using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TokenUser : EntityBase
    {
        // Mã bảo mật của người dùng
        public string? Token { get; set; }
        // Thời gian tạo mã bảo mật
        public DateTime? ExpiryDate { get; set; }
        // Mã thiết bị của người dùng (nếu có)
        public string? DeviceId { get; set; }   

        // User Infomation
        public long? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

    }
}
