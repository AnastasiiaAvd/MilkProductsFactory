using Milk.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Milk.BLL
{
    public class ExpensesHistoryProvider
    {
        public List<ExpensesHistoryDto> GetHistory()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var history = dbContext.geAllExpensesHistory().Select(p => new ExpensesHistoryDto
                {
                    HistoryId = p.Id,
                    Description = p.Description,
                    Sum=p.Sum,
                    Date=p.Date
                }).ToList();

                return history;
            }
        }
        public List<TaxDto> GetTaxes()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var taxes = dbContext.Taxes.ToList().Select(p => new TaxDto
                {
                    TaxId = p.TaxId,
                    ProductSellId=p.ProductSellId,
                    Product=dbContext.productSells.FirstOrDefault(l=>l.idProductSell==p.ProductSellId).Products.productName,
                    EmployeeName = dbContext.productSells.FirstOrDefault(l => l.idProductSell == p.ProductSellId).Employees.emloyeeName,
                    Sum = p.Sum,
                    Tax = p.Tax,
                    IsPaid = p.IsPaid

                }).ToList();

                return taxes;
            }
        }

        public List<PremiumDto> GetPremiums()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var premiums = dbContext.Premium.ToList().Select(p => new PremiumDto
                {
                    PremiumId = p.PremiumId,
                    ProductSellId = p.ProductSellId,
                    Product = dbContext.productSells.FirstOrDefault(l => l.idProductSell == p.ProductSellId).Products.productName,
                    EmployeeName = dbContext.productSells.FirstOrDefault(l => l.idProductSell == p.ProductSellId).Employees.emloyeeName,
                    Sum = p.Sum,
                    Premium = p.Premium1,
                    IsPaid = p.IsPaid

                }).ToList();

                return premiums;
            }
        }

        public bool PaySalary(out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {

                System.Data.Entity.Core.Objects.ObjectParameter isPay = new System.Data.Entity.Core.Objects.ObjectParameter("isPay", typeof(int));
                var data = dbContext.PaySalary(isPay);
                
                if (!Convert.ToBoolean(isPay.Value))
                {
                    errorMessage = $"Недостаточно средств в бюджете.";
                    return false;
                }
                return true;
            }
        }

        public bool PayRent(out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                System.Data.Entity.Core.Objects.ObjectParameter isPay = new System.Data.Entity.Core.Objects.ObjectParameter("isPay", typeof(bool));
                var data = dbContext.PayRent(isPay);

                if (!Convert.ToBoolean(isPay.Value))
                {
                    errorMessage = $"Недостаточно средств в бюджете.";
                    return false;
                }
                return true;
            }
        }
        public bool PayTax(out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                System.Data.Entity.Core.Objects.ObjectParameter isPay = new System.Data.Entity.Core.Objects.ObjectParameter("isPay", typeof(bool));
                var data = dbContext.PayTax(isPay);

                if (!Convert.ToBoolean(isPay.Value))
                {
                    errorMessage = $"Налоги оплачены или не недостаточно средств в бюджете!";
                    return false;
                }
                return true;
            }
        }
    }
}