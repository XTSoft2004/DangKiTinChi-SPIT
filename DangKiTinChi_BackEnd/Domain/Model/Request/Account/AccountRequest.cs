using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.Account
{
    public class AccountRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? SchoolEnum { get; set; }
    }
}
