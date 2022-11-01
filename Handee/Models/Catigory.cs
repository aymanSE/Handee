using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Handee.Models
{
    public partial class Catigory
    {
        public Catigory()
        {
            Products = new HashSet<Product>();
        }

        public decimal Catid { get; set; }
        public string Catname { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
