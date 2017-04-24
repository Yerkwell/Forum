using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Логин не введён")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Пароль не введён")]
        [DataType(DataType.Password)]
        public string Password { get; set;}
        public bool RememberMe { get; set; }
    }
}