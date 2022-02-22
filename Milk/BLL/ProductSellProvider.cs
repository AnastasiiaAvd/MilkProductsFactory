using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Milk.DataModels;

namespace Milk.BLL
{
    public class ProductSellProvider
    {
        public ProductSellDto GetProductSell(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var productSell = dbContext.productSells.FirstOrDefault(p => p.idProductSell == id);
                return new ProductSellDto
                {
                    ProductSellId = productSell.idProductSell,
                    ProductId = productSell.Products.idProduct,
                    Amount = productSell.amount,
                    Sum = productSell.sum,
                    Date = productSell.date,
                    EmployeeId = productSell.Employees.idEmployee,
                    EmployeeName = productSell.Employees.emloyeeName
                };
            }
        }
        public bool TryEditProductSell(ProductSellDto productSellDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                var addedCount = dbContext.productSells.FirstOrDefault(pr => pr.idProductSell == productSellDto.ProductSellId).amount;//amount в бд
                var totalCount = dbContext.Products.FirstOrDefault(pr => pr.idProduct == productSellDto.ProductId).Amount;//amount на складе

                if (productSellDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }

                if (totalCount < productSellDto.Amount-addedCount)
                {
                    errorMessage = $"Нельзя продать данное количество продукции! Количество продукции на складе: {totalCount}";
                    return false;
                }

                var sumBefore = dbContext.productSells.FirstOrDefault(pr => pr.idProductSell == productSellDto.ProductSellId).sum;
                var budget = dbContext.Budgets.FirstOrDefault(p => p.idBudget == 1).sum;
                if (sumBefore- productSellDto.Sum> budget)
                {
                    errorMessage = $"Недостаточно средств в бюджете.";
                    return false;
                }

                dbContext.updateProductSell(productSellDto.ProductSellId, productSellDto.ProductId, productSellDto.Amount, 
                    productSellDto.Sum, productSellDto.EmployeeId);
                return true;
            }
        }
        public List<ProductSellDto> GetProductSells()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var budget = dbContext.Budgets.FirstOrDefault(p => p.idBudget == 1).sum;
                var productSells = dbContext.getAllProductSells().Select(p => new ProductSellDto
                {
                  ProductSellId = p.idProductSell,
                  ProductName = p.productName,
                  ProductId = p.product,
                  Amount = p.amount,
                  Sum = p.sum,
                  Date = p.date,
                  EmployeeId = p.employee,
                  EmployeeName = p.emloyeeName,
                  ProductAmount=p.productAmount,
                  BudgetSum = budget
                }).ToList();
                return productSells;
            }
        }

        public bool TryDeleteProductSell(int id, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                var budget = dbContext.Budgets.FirstOrDefault(p => p.idBudget == 1).sum;
                var sell = dbContext.productSells.FirstOrDefault(pr => pr.idProductSell == id);
                if (budget-sell.sum<0)
                {
                    errorMessage = $"Недостаточно средств в бюджете.";
                    return false;
                }

                dbContext.deleteProductSell(id);
            }

            return true;
        }

        public bool TryAddProductSell(ProductSellDto productSellDto, out string errorMessage)
        {
            errorMessage = null;

            using (var dbContext = new MilkProductsEntities3())
            {

                var totalCount = dbContext.Products.FirstOrDefault(pr => pr.idProduct == productSellDto.ProductId).Amount;

                if (totalCount < productSellDto.Amount)
                {
                    errorMessage = $"Нельзя продать данное количество продукции! Количество продукции на складе: {totalCount}";
                    return false;
                }

                if (productSellDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }

                dbContext.addProductSell(productSellDto.ProductId, productSellDto.Amount, productSellDto.Sum, productSellDto.EmployeeId);
                return true;
            }
        }
    }
}