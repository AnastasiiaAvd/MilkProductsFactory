using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Milk.DataModels;

namespace Milk.BLL
{
    public class BudgetProvider
    {
        public BudgetDto GetBuget(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var budget = dbContext.getALLBudgets(id).FirstOrDefault();
                return new BudgetDto { BudgetId = budget.idBudget, Sum = budget.sum };
            }
        }
        public bool EditBudget(BudgetDto budgetDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (budgetDto.Sum < 0)
                {
                    errorMessage = $"Недопустимое значение суммы бюджета.";
                    return false;
                }

                dbContext.updateBudget(budgetDto.BudgetId, budgetDto.Sum);
                return true;
            }
        }
        public List<BudgetDto> GetBudgets()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var budgets = dbContext.getALLBudgets(1).Select(p => new BudgetDto
                {
                    BudgetId = p.idBudget,
                    Sum = p.sum
                }).ToList();
                return budgets;
            }
        }
        public bool TryPaySalary(int employeeId, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                var employeeSalary = dbContext.Employees.FirstOrDefault(p => p.idEmployee == employeeId).salary;
                var budget = dbContext.Budgets.FirstOrDefault(p => p.idBudget == 1).sum;
                if (budget-employeeSalary <= 0)
                {
                    errorMessage = $"Недостаточно средств в бюджете.";
                    return false;
                }

                errorMessage = $"Заработная плата выплачена.";
                var budgets = dbContext.Budgets.FirstOrDefault(p => p.idBudget == 1);
                budgets.sum = budgets.sum - employeeSalary;
                dbContext.SaveChanges();
                return true;
            }
        }
    }
}