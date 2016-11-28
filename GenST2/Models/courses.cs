namespace GenST2.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("courses")]
    public partial class course
    {
        [Key]
        public int courseID { get; set; }

        [Required]
        [StringLength(50)]
        public string courseDescription { get; set; }
    }
}
