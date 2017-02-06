using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GenST2.Models
{


    [Table("courses")]
    public partial class courses
    {
        [Key]
        public int courseID { get; set; }
        public string name { get; set; }

        public string description { get; set; }
        public string ages { get; set; }
        public int numweeks { get; set; }
        public DateTime entrydate { get; set; }
        public DateTime updatedate { get; set; }
        public int courseprice { get; set; }
    }
}
