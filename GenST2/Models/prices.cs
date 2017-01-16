using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenST2.Models
{
    public class prices
    {
        [Key]
        public int priceID { get; set; }
        public int pkgID { get; set; }
        public int priceperhr { get; set; }
    }
}