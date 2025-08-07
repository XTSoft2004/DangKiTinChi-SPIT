using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lecturer : EntityBase
    {
        /// Tên giảng viên
        public string? Name { get; set; }
        //public ICollection<Classes>? Classes { get; set; }
    }
}
