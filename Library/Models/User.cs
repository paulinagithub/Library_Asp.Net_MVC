using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] UserPassword { get; set; }
    }
}
