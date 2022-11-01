using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Handee.Models
{
    public partial class Product
    {
        public Product()
        {
            Sales = new HashSet<Sale>();
        }

        public decimal Productid { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        [Display(Name = "Image")]
        public string Imagepath { get; set; }

        public decimal? Userid { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public decimal? Catid { get; set; }
        public bool? Isavilable { get; set; }

        public virtual Catigory Cat { get; set; }
        public virtual Userr User { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
