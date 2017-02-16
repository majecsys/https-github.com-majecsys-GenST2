using System;
using System.ComponentModel.DataAnnotations;

namespace GenST2.Models
{
    public class StudentDetailsDisplayItems
    {
        [Key]
        public int studentID { get; set; }
        public int classcardID { get; set; }
        public string classDescription  { get; set; }
        public int courseID { get; set; }
        public string name { get; set; }
        public string firstname { get; set; }
        public DateTime expiration { get; set; }
        public DateTime classexpiration { get; set; }
        public int purchaseID { get; set; }

    }
}