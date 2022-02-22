using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class ProductSellDto
    {
        public int ProductSellId { get; set; }
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Укажите количество продукции"), 
         RegularExpression(@"^[0-9]+$", ErrorMessage = "Используйте только цифры")]
        public int Amount { get; set; }
        public double Sum { get; set; }
        public DateTime? Date { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ProductName { get; set; }
        public int ProductAmount { get; set; }
        public double? BudgetSum { get; set; }
    }
}