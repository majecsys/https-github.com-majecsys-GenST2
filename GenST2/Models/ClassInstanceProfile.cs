using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GenST2.Models
{
    public partial class ClassInstanceProfile
    {
        [Key]
        public int id { get; set; }
        public int studentID { get; set; }
        public int classID { get; set; }
        public int remainingInstances { get; set; }
        public DateTime recDate { get; set; }
    }
}