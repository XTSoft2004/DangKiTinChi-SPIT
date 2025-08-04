using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.Class
{
    public class ClassUpdateRequest
    {
        public string? Name { get; set; }
        public int? Day { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }
        public string? Room { get; set; }
        public long? MaxStudent { get; set; }

        public long? LecturerId { get; set; }
        public long? CourseId { get; set; }
    }
}
