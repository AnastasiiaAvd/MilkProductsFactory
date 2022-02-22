using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Milk.DataModels;

namespace Milk.BLL
{
    public class ProductionProvider
    {
        public ProductionDto GetProduction(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var production = dbContext.Production.FirstOrDefault(p => p.idProduction == id);
                return new ProductionDto
                {
                    ProductionId = production.idProduction,
                    ProductId = production.Products.idProduct,
                    ProductName = production.Products.productName,
                    Amount = production.amount,
                    Date = production.date,
                    EmployeeId = production.Employees.idEmployee,
                    EmployeeName = production.Employees.emloyeeName
                };
            }
        }
        public bool TryEditProduction(ProductionDto productionDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (productionDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }

                dbContext.updateProduction(productionDto.ProductionId, productionDto.ProductId, productionDto.Amount, productionDto.EmployeeId);
                return true;
            }
        }
        public List<ProductionDto> GetProductions()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var budget = dbContext.Budgets.FirstOrDefault(p => p.idBudget == 1).sum;
                var productions = dbContext.getAllProduction().Select(p => new ProductionDto
                {
                   ProductionId = p.idProduction,
                   ProductId = p.product,
                   ProductName = p.productName,
                   Amount = p.amount,
                   Date = p.date,
                   EmployeeId = p.employee,
                   EmployeeName = p.emloyeeName,
                   ProductAmount=p.productAmount,
                   BudgetSum = budget
                }).ToList();
                return productions;
            }
        }

        public void DeleteProduction(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                dbContext.deleteProduction(id);
            }
        }

        public bool TryAddProduction(ProductionDto productionDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (productionDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }

                dbContext.addProduction(productionDto.ProductId, productionDto.Amount, productionDto.EmployeeId);
                return true;
            }
        }
    }
}