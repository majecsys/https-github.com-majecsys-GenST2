﻿using System;
using System.ComponentModel.DataAnnotations;


namespace GenST2.Models
{
    public class purchases
    {
        [Key]
        public int purchaseID { get; set; }
        public int studentID { get; set; }
        public int scheduleID { get; set; }
        public int pkgID { get; set; }
        public int paymentID { get; set; }
        public DateTime attendancedate{ get; set; }
        public DateTime expirationdate { get; set; }
        public DateTime purchasedate { get; set; }
        public DateTime entrydate { get; set; }
        public DateTime updatedate { get; set; }
    }
}