using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class IngredientDto
    {
        public int IngredientId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int RawId { get; set; }
        public string RawName { get; set; }

        [Required(ErrorMessage = "Укажите количество сырья"),
         RegularExpression(@"^[0-9]+$", ErrorMessage = "Используйте только цифры")]
        public int Amount { get; set; }
        public int RawMaterialAmount { get; set; }
    }
}