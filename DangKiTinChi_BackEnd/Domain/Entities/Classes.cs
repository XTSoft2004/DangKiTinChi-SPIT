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
        public string? Name { get; set; }
        // Thứ học
        public int? Day { get; set; }
        // Thời gian bắt đầu
        public int? StartTime { get; set; }
        // Thời gian kết thúc
        public int? EndTime { get; set; }
        // Mã phòng học
        public string? Room { get; set; }
        // Số lượng tối đa sinh viên
        public long? MaxStudent { get; set; }

        public long? LecturerId { get; set; }
        [ForeignKey(nameof(LecturerId))]
        public Lecturer? Lecturer { get; set; }

        public long? CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Courses? Course { get; set; }
    }
}
