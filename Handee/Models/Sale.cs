using System;
using System.Collections.Generic;

#nullable disable

namespace Handee.Models
{
    public partial class Sale
    {
        public decimal Sid { get; set; }
        public DateTime? Datesold { get; set; }
        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Productid { get; set; }
        public decimal? Userid { get; set; }

        public virtual Product Product { get; set; }
        public virtual Userr User { get; set; }
    }
}
