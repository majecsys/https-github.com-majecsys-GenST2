using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GenST2.Models
{


    [Table("classcard")]
    public partial class classcard
    {
        [Key]
        public int classcardID { get; set; }
        public int price { get; set; }

        public string description { get; set; }
        public int numclasses { get; set; }
        public DateTime purchasedate { get; set; }
        public DateTime updatedate { get; set; }
        public int cardLength { get; set; }
    }
}
