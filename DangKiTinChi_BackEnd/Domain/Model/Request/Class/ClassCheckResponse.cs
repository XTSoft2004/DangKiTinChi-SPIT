using Domain.Model.Request.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.Class
{
    public class ClassCheckResponse
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public List<TimeRequest>? TimesRequest { get; set; }
        public long? MaxStudent { get; set; }
        public string? CourseName { get; set; }
    }
}
