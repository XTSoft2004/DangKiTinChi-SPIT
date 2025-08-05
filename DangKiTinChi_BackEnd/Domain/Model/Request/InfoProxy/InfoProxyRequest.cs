using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.InfoProxy
{
    public class InfoProxyRequest
    {
        public string? Proxy { get; set; }
        public string? TypeProxy { get; set; }
        public string? Status { get; set; }
    }
}
