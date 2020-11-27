using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
