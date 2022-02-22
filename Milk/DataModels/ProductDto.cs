using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Укажите наименование продукции"),
         RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Наименование содержит только буквы")]
        public string ProductName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }

        [Required(ErrorMessage = "Укажите количество продукции"),
         RegularExpression(@"^[0-9]+$", ErrorMessage = "Используйте только цифры")]
        public int Amount { get; set; }
        public double Sum { get; set; }
        public double Cost { get; set; }
    }
}