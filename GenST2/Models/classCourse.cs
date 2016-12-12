using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenST2.Models
{
    public class classCourse
    {
        [Key]
        public int sid { get; set; }
        public int cid { get; set; }
        public int coid { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }


}