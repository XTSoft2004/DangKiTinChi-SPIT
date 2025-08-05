using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.AccountClass
{
    public class AccountClassRequest
    {
        public long? AccountId { get; set; }
        public long? ClassesId { get; set; }
        public string? StatusRegister { get; set; }
        public int? CountFailed { get; set; }
        public string? __RequestVerificationToken { get; set; }
        public string? Status { get; set; }
        public long? InfoProxyId { get; set; }
    }
}
