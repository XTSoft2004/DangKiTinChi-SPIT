using Domain.Common;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AccountClasses : EntityBase
    {
        // Liên kết với Account
        [ForeignKey(nameof(AccountId))]
        public long? AccountId { get; set; }
        public Account? Account { get; set; }
        // Liên kết với Classes
        [ForeignKey(nameof(ClassesId))]
        public long? ClassesId { get; set; }
        public Classes? Classes { get; set; }

        // Trạng thái đăng ký lớp học
        public Register_Enum StatusRegister { get; set; }
        // Số lượt đăng kí thất bại
        public int? CountFailed { get; set; }
        // Trạng thái hiện tại của lớp
        public string? Status { get; set; }

        // Liên kết với InfoProxy (nếu có)

        [ForeignKey(nameof(InfoProxyId))]
        public long? InfoProxyId { get; set; }
        public InfoProxy? InfoProxy { get; set; }
    }
}
