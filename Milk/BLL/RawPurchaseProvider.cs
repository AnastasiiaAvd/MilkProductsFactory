using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Milk.DataModels;

namespace Milk.BLL
{
    public class RawPurchaseProvider
    {
        public RawPurchaseDto GetRawPurchase(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var rawPurchase = dbContext.rawPurchases.FirstOrDefault(p => p.idRawPurchase == id);
                return new RawPurchaseDto()
                {
                   RawPurchaseId = rawPurchase.idRawPurchase,
                   RawId = rawPurchase.RawMaterials.idRaw,
                   RawName = rawPurchase.RawMaterials.rawName,
                   Amount = rawPurchase.amount,
                   Sum = rawPurchase.sum,
                   Date = rawPurchase.date,
                   EmployeeId = rawPurchase.Employees.idEmployee,
                   EmployeeName = rawPurchase.Employees.emloyeeName,
                };
            }
        }
        public bool TryEditRawPurchase(RawPurchaseDto rawPurchaseDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (rawPurchaseDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }

                var budget = dbContext.Budgets.FirstOrDefault(p => p.idBudget == 1).sum;
                var rawMaterialPrice = dbContext.RawMaterials.FirstOrDefault(p => p.idRaw == rawPurchaseDto.RawId).sum;

                if (rawPurchaseDto.Amount*rawMaterialPrice > budget)
                {
                    errorMessage = $"Недостаточно средств на закупку сырья в количестве: {rawPurchaseDto.Amount}";
                    return false;
                }

                dbContext.updateRawPurchase(rawPurchaseDto.RawPurchaseId, rawPurchaseDto.RawId, rawPurchaseDto.Amount,
                   rawPurchaseDto.Sum, rawPurchaseDto.EmployeeId);
            }

            return true;
        }
        public List<RawPurchaseDto> GetRawPurchases()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var budget = dbContext.Budgets.FirstOrDefault(p => p.idBudget == 1).sum;
                var rawPurchases = dbContext.getAllRawPurchases().Select(p => new RawPurchaseDto
                {
                    RawPurchaseId = p.idRawPurchase,
                    RawId = p.rawMaterial,
                    RawName = p.rawName,
                    Amount = p.amount,
                    Sum = p.sum,
                    Date =p.date,
                    EmployeeId = p.employee,
                    EmployeeName = p.emloyeeName,
                    RawMaterialAmount = p.rawAmount,
                    BudgetSum=budget
                }).ToList();
                return rawPurchases;
            }
        }

        public void DeleteRawPurchase(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                dbContext.deleteRawPurchase(id);
            }
        }

        public bool TryAddPurchase(RawPurchaseDto rawPurchaseDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (rawPurchaseDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }

                var budget = dbContext.Budgets.FirstOrDefault(p => p.idBudget == 1).sum;
                var rawMaterialPrice = dbContext.RawMaterials.FirstOrDefault(p => p.idRaw == rawPurchaseDto.RawId).sum;
                if (rawPurchaseDto.Amount * rawMaterialPrice > budget)
                {
                    errorMessage = $"Недостаточно средств на закупку сырья в количестве: {rawPurchaseDto.Amount}";
                    return false;
                }

                dbContext.addRawPurchase(rawPurchaseDto.RawId, rawPurchaseDto.Amount,
                   rawPurchaseDto.Sum, rawPurchaseDto.EmployeeId);
            }
            return true;
        }
    }
}