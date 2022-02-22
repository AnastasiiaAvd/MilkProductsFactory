using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }

        [Required(ErrorMessage = "Укажите сумму бюджета"),
         RegularExpression(@"^[0-9]+$", ErrorMessage = "Используйте только цифры")]
        public double? Sum { get; set; }
    }
}