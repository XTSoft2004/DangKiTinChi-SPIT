using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TimeClass : EntityBase
    {   
        public long ClassId { get; set; }
        public virtual Classes? Classes { get; set; }
        public long TimeId { get; set; }
        public virtual Time? Times { get; set; }
    }
}
