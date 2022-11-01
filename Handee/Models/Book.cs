using System;
using System.Collections.Generic;

#nullable disable

namespace Handee.Models
{
    public partial class Book
    {
        public decimal Bookid { get; set; }
        public decimal? Price { get; set; }
        public string Bookname { get; set; }
        public string Bookdesc { get; set; }
        public string Anotherinfo { get; set; }
    }
}
