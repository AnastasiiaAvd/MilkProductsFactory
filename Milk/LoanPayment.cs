//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Milk
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoanPayment
    {
        public long IdCredit { get; set; }
        public int PayoutNumber { get; set; }
        public double AmountPaymentCredit { get; set; }
        public double AmountPaymentProcent { get; set; }
        public double AmountTotal { get; set; }
        public double LoanBalance { get; set; }
        public System.DateTime RequiredDate { get; set; }
        public Nullable<System.DateTime> ActualDate { get; set; }
        public Nullable<int> DelayedBy { get; set; }
        public Nullable<double> Penalty { get; set; }
    }
}
