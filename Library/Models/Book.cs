using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public string Author { get; set; }
        public int? PublishmentYear { get; set; }
        public string ShortDescription { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
