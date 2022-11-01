using System;
using System.Collections.Generic;

#nullable disable

namespace Handee.Models
{
    public partial class Visa
    {
        public decimal Id { get; set; }
        public string Pin { get; set; }
        public string Pass { get; set; }
        public string Exp { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Balance { get; set; }

        public virtual Userr User { get; set; }
    }
}
