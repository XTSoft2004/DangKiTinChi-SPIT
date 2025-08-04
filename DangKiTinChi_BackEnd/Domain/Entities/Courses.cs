using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Courses : EntityBase
    {
        // Mã môn học
        public string? Code { get; set; }
        // Tên môn học
        public string? Name { get; set; }
        // Số tín chỉ của môn học
        public int? Credit { get; set; }

        public long? DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        public ICollection<Classes>? Classes { get; set; }
    }
}
