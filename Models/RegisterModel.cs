using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Логин не введён")]
        public String Login { get; set; }
        [Required(ErrorMessage = "Пароль не введён")]
        [MinLength(6, ErrorMessage = "Пароль должен быть не короче 6 символов")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required(ErrorMessage = "Пароль не введён")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public String Email { get; set; }
    }
}