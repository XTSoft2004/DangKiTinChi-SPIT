using Domain.Model.Response.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response.Class
{
    public class ClassResponse
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public long? MaxStudent { get; set; }
        public string? CourseName { get; set; }
        public List<TimeResponse>? TimeClasses { get; set; }

    }
}
