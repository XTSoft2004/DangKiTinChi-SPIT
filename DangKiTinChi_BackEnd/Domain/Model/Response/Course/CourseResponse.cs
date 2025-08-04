using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response.Course
{
    public class CourseResponse
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? Credit { get; set; }

        public string? DepartmentName { get; set; }
    }
}
