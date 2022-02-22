using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class TaxDto
    {
        public int TaxId { get; set; }
        public int ProductSellId { get; set; }
        public string Product { get; set; }
        public string EmployeeName { get; set; }
        public double Sum { get; set; }
        public double? Tax { get; set; }
        public bool? IsPaid { get; set; }
    }
}