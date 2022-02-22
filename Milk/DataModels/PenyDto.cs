using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class PenyDto
    {
        public long CreditId { get; set; }
        public int? PayoutNumber { get; set; }
        public DateTime? ActualDate { get; set; }
    }
}