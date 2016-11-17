namespace GenST2.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("classes")]
    public partial class _class
    {
        public int classID { get; set; }

        [Required]
        [StringLength(50)]
        public string classDescriptions { get; set; }
    }
}
