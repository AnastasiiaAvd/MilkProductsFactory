using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class RawMaterialDto
    {
        public int RawId { get; set; }
        [Required(ErrorMessage = "Укажите наименование сырья")]
        public string RawName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }

        [Required(ErrorMessage = "Укажите стоимость сырья"),
         RegularExpression(@"^[0-9'','\s]+$", ErrorMessage = "Используйте только цифры")]
        public double Sum { get; set; }

        [Required(ErrorMessage = "Укажите количество сырья"),
         RegularExpression(@"^[0-9]+$", ErrorMessage = "Используйте только цифры")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Укажите наценку на сырье"),
         RegularExpression(@"^[0-9'','\s]+$", ErrorMessage = "Используйте только цифры")]
        public double Cost { get; set; }
    }
}