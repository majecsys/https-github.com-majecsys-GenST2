using System.ComponentModel.DataAnnotations;

namespace GenST2.Models
{
    public partial class REFCourse
    {
        [Key]
        public int courseID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string ages { get; set; }
    }
}