using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenST2.Models
{
    public partial class students
    {
        [Key]
        public int studentID { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        
        public string email { get; set; }
      
        public DateTime  entrydate { get; set; }
       
        public DateTime updatedate { get; set; }
    }
}