namespace GenST2.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("checkin")]
    public partial class checkin
    {
        public int id { get; set; }

        public bool currentStudent { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string Lastname { get; set; }

        [StringLength(50)]
        public string FamilyName { get; set; }

        public DateTime StartDate { get; set; }

        public bool present { get; set; }
    }
}
