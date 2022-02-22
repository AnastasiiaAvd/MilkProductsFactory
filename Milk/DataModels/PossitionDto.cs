using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    /// <summary>
    /// Модель данных по должностям
    /// </summary>
    public class PossitionDto
    {
       public int PositionId { get; set; }

       [Required(ErrorMessage = "Укажите наименование должности")]
        public string PosotionName { get; set; }
    }
}