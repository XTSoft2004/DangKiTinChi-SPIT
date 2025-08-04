using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.Course
{
    public class CourseUpdateRequest
    {
        public string? Name { get; set; }
        public int? Credit { get; set; }

        public long? DepartmentId { get; set; }
    }
}
