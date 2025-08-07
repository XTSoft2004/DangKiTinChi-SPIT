using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Classes : EntityBase
    {
        // Mã lớp học
        public string? Code { get; set; }
        // Tên lớp học
        public string? Name { get; set; }
        // Số lượng tối đa sinh viên
        public long? MaxStudent { get; set; }

        public long? CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Courses? Course { get; set; }
        public ICollection<TimeClass>? TimeClasses { get; set; }
    }
}
