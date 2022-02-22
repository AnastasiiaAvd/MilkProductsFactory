using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class LoanPaymentDto
    {
        public long CreditId { get; set; }
        public int? PayoutNumber { get; set; }
        public double AmountPaymentCredit { get; set; }
        public double AmountPaymentProcent { get; set; }
        public double AmountTotal { get; set; }
        public double LoanBalance { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public int? DelayedBy { get; set; }
        public double? Penalty { get; set; }

    }
}