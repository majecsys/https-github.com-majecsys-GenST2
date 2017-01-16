using System;
using System.ComponentModel.DataAnnotations;


namespace GenST2.Models
{
    public partial class REFClass
    {
        [Key]
        public int classID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int ages { get; set; }
    }
}