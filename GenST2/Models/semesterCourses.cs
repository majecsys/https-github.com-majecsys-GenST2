using System;
using System.ComponentModel.DataAnnotations;

namespace GenST2.Models
{
    public class semesterCourses
    { 
        [Key]
        public int scID { get; set; }
        public string description { get; set; }
        public string season { get; set; }
        public DateTime purchasedate { get; set; }
        public int price { get; set; }
    }
}