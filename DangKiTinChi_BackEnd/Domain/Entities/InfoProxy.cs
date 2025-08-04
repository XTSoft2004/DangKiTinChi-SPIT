using Domain.Common;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InfoProxy : EntityBase
    {
        public string? Proxy { get; set; }
        public string? TypeProxy { get; set; }
        public Status_Proxy_Enum Status { get; set; } = Status_Proxy_Enum.Active;

        public ICollection<AccountClasses>? AccountClasses { get; set; }
    }
}
