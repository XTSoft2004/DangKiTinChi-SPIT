using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response.HistoryMoney
{
    public class HistoryMoneyResponse
    {
        public long Id { get; set; }
        public long? Money { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public long? UserId { get; set; }
    }
}
