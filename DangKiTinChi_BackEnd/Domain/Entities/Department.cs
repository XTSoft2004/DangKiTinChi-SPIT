using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Department : EntityBase
    {
        // Mã khoa
        [Required, StringLength(50)]
        public string? Code { get; set; }
        // Tên khoa
        [Required, StringLength(100)]
        public string? Name { get; set; }

        public ICollection<Courses>? Courses { get; set; }
    }
}
