using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class EditMessageModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage="Введите текст")]
        public String Text { get; set; }
    }
}