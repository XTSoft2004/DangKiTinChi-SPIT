using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Time : EntityBase
    {
        // Thứ học
        public int? Day { get; set; }
        // Thời gian bắt đầu
        public int? StartTime { get; set; }
        // Thời gian kết thúc
        public int? EndTime { get; set; }
        // Mã phòng học
        public string? Room { get; set; }

        public ICollection<TimeClass>? TimeClasses { get; set; }
    }
}
