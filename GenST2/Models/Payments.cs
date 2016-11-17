namespace GenST2.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class payments
    {
        public int Id { get; set; }

        public int studentID { get; set; }

        [Column(TypeName = "money")]
        public decimal fees { get; set; }

        [Required]
        [StringLength(15)]
        public string paymentType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? paymentDate { get; set; }
    }
}
