namespace GenST2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;
    public partial class ClassCourseElements : DbContext
    {
        //internal readonly object checkin;

        public ClassCourseElements()
            : base("name=dbCourseClass")
        {
        }

        public virtual DbSet<_class> classes { get; set; }
        public virtual DbSet<course> courses { get; set; }
        public virtual DbSet<payments> payments { get; set; }
        public virtual DbSet<students> students { get; set; }
        public virtual DbSet<checkins> checkins { get; set; }
        public virtual DbSet <classCourse> classCourse { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
