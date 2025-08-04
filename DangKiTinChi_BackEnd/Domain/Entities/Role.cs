using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role : EntityBase
    {
        // Tên quyền hạn
        [Required, StringLength(100)]
        public string? DisplayName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
