using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class User
    {
        [Required(ErrorMessage = "Login jest wymagane")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Passward { get; set; }

    }
}
