using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModel
{
    public class UserViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
