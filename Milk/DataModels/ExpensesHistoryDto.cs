using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class ExpensesHistoryDto
    {
        public int HistoryId { get; set; }
        public string Description { get; set; }
        public double Sum { get; set; }
        public DateTime Date { get; set; }
    }
}