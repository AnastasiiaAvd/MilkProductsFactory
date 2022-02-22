using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Milk.DataModels;

namespace Milk.BLL
{
    public class RawPurchaseDto
    {
        public int RawPurchaseId { get; set; }
        public int RawId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string RawName { get; set; }

        [Required(ErrorMessage = "Укажите количество"),
         RegularExpression(@"^[0-9]+$", ErrorMessage = "Используйте только цифры")]
        public int Amount { get; set; }
        public double Sum { get; set; }
        public DateTime? Date { get; set; }
        public int RawMaterialAmount { get; set; }
        public double? BudgetSum { get; set; }
    }
}