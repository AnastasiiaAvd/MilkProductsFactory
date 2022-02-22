using Milk.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milk.BLL
{
    public class LoanPaymentProvider
    {
        public List<LoanPaymentDto> GetLoanPayments()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var loans = dbContext.LoanPayment.ToList().Select(p => new LoanPaymentDto
                {
                    CreditId=p.IdCredit,
                    PayoutNumber=p.PayoutNumber,
                    AmountPaymentCredit=p.AmountPaymentCredit,
                    AmountPaymentProcent=p.AmountPaymentProcent,
                    AmountTotal=p.AmountTotal,
                    LoanBalance=p.AmountTotal,
                    RequiredDate=p.RequiredDate,
                    ActualDate=p.ActualDate,
                    DelayedBy=p.DelayedBy,
                    Penalty=p.Penalty
                }).ToList();
                return loans;
            }
        }

        public List<int> GetLoan(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var loans = dbContext.LoanPayment.Where(p => p.IdCredit == id && p.Penalty==null ).Select(p => p.PayoutNumber).ToList();
                return loans;
            }
        }
        public bool PayLoan(LoanPaymentDto loanPaymentDto,out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {

                System.Data.Entity.Core.Objects.ObjectParameter isPay = new System.Data.Entity.Core.Objects.ObjectParameter("isPay", typeof(int));
                var data = dbContext.PayCredit(loanPaymentDto.CreditId,loanPaymentDto.PayoutNumber, loanPaymentDto.ActualDate,isPay);

                if (!Convert.ToBoolean(isPay.Value))
                {
                    errorMessage = $"Недостаточно средств в бюджете.";
                    return false;
                }
                return true;
            }
        }

        public string CalculatePenalty(PenyDto penyDto)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                System.Data.Entity.Core.Objects.ObjectParameter peny = new System.Data.Entity.Core.Objects.ObjectParameter("peny", typeof(double));
                var penalty = dbContext.Diff(Convert.ToInt32(penyDto.CreditId), penyDto.PayoutNumber, penyDto.ActualDate, peny);
                return Convert.ToString(peny.Value);
            }
        }
    }
}