using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milk.DataModels
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Укажите имя сотрудника")]
        public string EmployeeName { get; set; }
        public int PositionId { get; set; }
        public string PosotionName { get; set; }

        [Required(ErrorMessage = "Укажите заработную плату"),
         RegularExpression(@"^[0-9]+$", ErrorMessage = "Используйте только цифры")]
        public double? Salary { get; set; }

        [Required(ErrorMessage = "Укажите адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона"),
         RegularExpression(@"^[0-9]+$", ErrorMessage = "Используйте только цифры")]
        public string PhoneNumber { get; set; }
    }
}