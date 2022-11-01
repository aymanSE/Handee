using System;
using System.Collections.Generic;

#nullable disable

namespace Handee.Models
{
    public partial class Role
    {
        public Role()
        {
            Userlogins = new HashSet<Userlogin>();
            Userrs = new HashSet<Userr>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Userlogin> Userlogins { get; set; }
        public virtual ICollection<Userr> Userrs { get; set; }
    }
}
