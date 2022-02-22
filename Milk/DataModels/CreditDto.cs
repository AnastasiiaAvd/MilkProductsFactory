using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class CreditDto
    {
        public long CreditId { get; set; }
        public double CreditAmount { get; set; }
        public double CreditRate { get; set; }
        public int Term { get; set; }
        public DateTime DateOfTaking { get; set; }
    }
}