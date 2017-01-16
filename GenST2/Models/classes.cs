using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GenST2.Models
{


    [Table("classes")]
    public partial class classes
    {
        [Key]
        public int pkgID { get; set; }

        public string description { get; set; }
        public int numclasses { get; set; }
        public string classtype { get; set; }
        public int timelimit { get; set; }
        public DateTime entrydate { get; set; }
        public DateTime updatedate { get; set; }
    }
}
