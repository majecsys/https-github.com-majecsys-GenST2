namespace GenST2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("checkin")]
    public partial class StuCheckin
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
