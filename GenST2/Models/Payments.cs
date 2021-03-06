namespace GenST2.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class payments
    {
        [Key]
        public int paymentID { get; set; }

        public int studentID { get; set; }
        public int classID { get; set; }
     //   [DataType(DataType.Currency)]
        public int amount { get; set; }

        [Required]
        [StringLength(15)]
        public string paymentType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? paymentDate { get; set; }
    }
}
