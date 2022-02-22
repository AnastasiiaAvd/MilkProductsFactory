using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    /// <summary>
    /// Модель данных по единицам измерения
    /// </summary>
    public class UnitDto
    {
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Укажите наименование единицы измерения"), 
         RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Наименование содержит только буквы")]
        public string UnitName { get; set; }
    }
}