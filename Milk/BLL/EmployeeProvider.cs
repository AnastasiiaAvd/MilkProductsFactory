using Microsoft.EntityFrameworkCore;
using Milk.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WebGrease.Css.Extensions;

namespace Milk.BLL
{
    public class EmployeeProvider
    {
        public EmployeeDto GetEmployee(int id)
        {
            using (var dbContext= new MilkProductsEntities3())
            {
                var x = dbContext.Employees.FirstOrDefault(p => p.idEmployee == id);
                 return new EmployeeDto
                {
                    EmployeeId = x.idEmployee,
                    EmployeeName = x.emloyeeName,
                    PositionId = x.Positions.idPosition,
                    PosotionName = x.Positions.posotionName,
                    Salary = x.salary,
                    Address = x.address,
                    PhoneNumber = x.phoneNumber
                };    
            }
      
        }

        public void EditEmployee(EmployeeDto employeeDto)
        {
            using (var dbContext=new MilkProductsEntities3())
            {
                dbContext.updateEmployee(employeeDto.EmployeeId, employeeDto.EmployeeName, employeeDto.PositionId, employeeDto.Salary,
                   employeeDto.Address, employeeDto.PhoneNumber);
            }
        }

        public List<EmployeeDto> GetEmployees()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var employees = dbContext.getAllEmployees().Select(employee => new EmployeeDto
                {
                    EmployeeId = employee.idEmployee,
                    EmployeeName = employee.emloyeeName,
                    PositionId = employee.position,
                    PosotionName = employee.posotionName,
                    Salary = employee.salary,
                    Address = employee.address,
                    PhoneNumber = employee.phoneNumber
                }).ToList();
                return employees;
            }
        }

        public bool TryDeleteEmployee(int id, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext= new MilkProductsEntities3() )
            {
                var employee = dbContext.Employees.FirstOrDefault(p => p.idEmployee == id);

                var productSell = dbContext.productSells.FirstOrDefault(e => e.employee == employee.idEmployee);

                var production = dbContext.Production.FirstOrDefault(e => e.employee == employee.idEmployee);

                var rawPurchase = dbContext.rawPurchases.FirstOrDefault(e => e.employee == employee.idEmployee);

                if (productSell != null)
                {
                    errorMessage =
                        $"Нельзя удалить сотрудника '{employee.emloyeeName}', так как он участвует в продаже продукции '{productSell.Products.productName}'";
                    return false;
                }
                if (production != null)
                {
                    errorMessage =
                        $"Нельзя удалить сотрудника '{employee.emloyeeName}', так как он участвует в производстве продукции '{production.Products.productName}'";
                    return false;
                }
                if (rawPurchase != null)
                {
                    errorMessage =
                        $"Нельзя удалить сотрудника '{employee.emloyeeName}', так как он участвует в закупке сырья '{rawPurchase.RawMaterials.rawName}'";
                    return false;
                }

                dbContext.deleteEmployee(id);
                return true;
            }
        }

        public void AddEmployee(EmployeeDto employee)
        {
            using(var dbContext=new MilkProductsEntities3())
            {
                dbContext.addEmployee(employee.EmployeeName, employee.PositionId, employee.Salary, employee.Address,  employee.PhoneNumber );
            }
        }
    }
}