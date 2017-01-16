namespace GenST2.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class studentsold
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

    //    [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}") ]
        public DateTime StartDate { get; set; }

        public string classID { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public short remainingClasses { get; set; }

        public string courseID { get; set; }

        public short? amtPaid { get; set; }
    }
}
