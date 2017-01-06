using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenST2.Models
{
    public class checkins
    {
        public int id { get; set; }
        public int studentID { get; set; }

        public bool currentStudent { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string Lastname { get; set; }

        [StringLength(50)]
        public string FamilyName { get; set; }

        [StringLength(50)]
        public string classDesc { get; set; }

        public int classID { get; set; }

        [StringLength(50)]
        public string courseDesc { get; set; }

        public DateTime StartDate { get; set; }

        public bool present { get; set; }
    }
}
