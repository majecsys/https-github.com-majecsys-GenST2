using System;
using System.ComponentModel.DataAnnotations;

namespace GenST2.Models
{
    public class StudentDetailsDisplayItems
    {
        [Key]
        public int studentID { get; set; }
        public int pkgID { get; set; }
        public string classDescription  { get; set; }
        public string firstname { get; set; }


    }
}