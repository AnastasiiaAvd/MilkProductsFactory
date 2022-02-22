using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class PremiumDto
    {
        public int PremiumId { get; set; }
        public int ProductSellId { get; set; }
        public string Product { get; set; }
        public string EmployeeName { get; set; }
        public double Sum { get; set; }
        public double? Premium { get; set; }
        public bool? IsPaid { get; set; }
    }
}