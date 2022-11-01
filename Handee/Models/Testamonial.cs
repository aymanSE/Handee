using System;
using System.Collections.Generic;

#nullable disable

namespace Handee.Models
{
    public partial class Testamonial
    {
        public string Note { get; set; }
        public decimal Tid { get; set; }
        public decimal? Userid { get; set; }

        public virtual Userr User { get; set; }
    }
}
