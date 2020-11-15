using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Library.Models
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        [Display(Name = "Data rezerwacji")]
        public DateTime ReservationDate { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
