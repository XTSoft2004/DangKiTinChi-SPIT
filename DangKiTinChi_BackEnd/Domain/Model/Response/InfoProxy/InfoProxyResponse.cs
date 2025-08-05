using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response.InfoProxy
{
    public class InfoProxyResponse
    {
        public long Id { get; set; }
        public string? Proxy { get; set; }
        public string? TypeProxy { get; set; }
        public string? Status { get; set; }
    }
}
