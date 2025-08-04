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
    public class HistoryMoney : EntityBase
    {
        // Số tiền thay đổi
        public long? Money { get; set; }
        // Lý do thay đổi
        [Required, StringLength(100)]
        public string? Title { get; set; }
        // Mô tả chi tiết về thay đổi
        [StringLength(100)]
        public string? Description { get; set; }

        public long? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
