using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.HistoryMoney
{
    public class HistoryMoneyRequest
    {
        public long? Money { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public long? UserId { get; set; }
    }
}
