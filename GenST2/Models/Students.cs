namespace GenST2.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class students
    {
        [StringLength(50)]
        public string firstname { get; set; }

        [StringLength(50)]
        public string lastname { get; set; }

        [StringLength(50)]
        public string familyname { get; set; }
        [Key]
        public int id { get; set; }

        public bool didPay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        public int classID { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public short remainingClasses { get; set; }

        public int courseID { get; set; }

        public short? amtPaid { get; set; }
    }
}
