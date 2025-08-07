using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.Time
{
    public class TimeRequest
    {
        public int? Day { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }
        public string? Room { get; set; }
    }
}
