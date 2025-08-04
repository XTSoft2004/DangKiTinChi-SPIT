using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response.Account
{
    public class AccountResponse
    {
        public long? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Cookie { get; set; }
        public string? SemeterName { get; set; }
        public string? DomainSchool { get; set; }
        public string? __RequestVerificationToken { get; set; }
    }
}
