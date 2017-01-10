using System.ComponentModel.DataAnnotations;

namespace GenST2.Models
{
    public class CheckinLVDisplayItems
    {
        [Key]
        public int studentID { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string courseDesc { get; set; }
        public string classDesc { get; set; }
        public int remainingInstances { get; set; }
        public int classID { get; set; }
    }
}