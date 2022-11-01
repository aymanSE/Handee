using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Handee.Models
{
    public partial class Userr
    {
        public Userr()
        {
            Products = new HashSet<Product>();
            Sales = new HashSet<Sale>();
            Testamonials = new HashSet<Testamonial>();
            Userlogins = new HashSet<Userlogin>();
            Visas = new HashSet<Visa>();
        }

        public decimal Userid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Imagepath { get; set; }
        public string Email { get; set; }
        public decimal? Roleid { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Testamonial> Testamonials { get; set; }
        public virtual ICollection<Userlogin> Userlogins { get; set; }
        public virtual ICollection<Visa> Visas { get; set; }
    }
}
