using System;
using System.Collections.Generic;

#nullable disable

namespace Handee.Models
{
    public partial class Userlogin
    {
        public decimal Id { get; set; }
        public string Usernsme { get; set; }
        public string Password { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Roleid { get; set; }

        public virtual Role Role { get; set; }
        public virtual Userr User { get; set; }
    }
}
