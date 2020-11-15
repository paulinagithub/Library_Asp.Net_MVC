using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Library.Models
{
    public partial class Book
    {
        public Book()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int BookId { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [Display(Name = "Rok wydania")]
        public int? PublishmentYear { get; set; }
        [Display(Name = "Krótki Opis")]
        public string ShortDescription { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
