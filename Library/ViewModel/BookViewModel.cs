using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModel
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Rok musi zawierać 4 cyfry")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Rok wydania")]
        public int? PublishmentYear { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Krótki Opis")]
        public string ShortDescription { get; set; }
    }
}
