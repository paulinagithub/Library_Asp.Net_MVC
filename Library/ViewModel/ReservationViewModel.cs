using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModel
{
    public class ReservationViewModel
    {
        public int BookId { get; set; }
        [Display(Name = "Data rezerwacji")]
        public DateTime ReservationDate { get; set; }

        public virtual BookViewModel Book { get; set; }
    }
}
