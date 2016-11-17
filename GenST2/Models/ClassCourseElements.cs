namespace GenST2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ClassCourseElements : DbContext
    {
        public ClassCourseElements()
            : base("name=dbCourseClass")
        {
        }

        public virtual DbSet<_class> classes { get; set; }
        public virtual DbSet<course> courses { get; set; }
        public virtual DbSet<payments> payments { get; set; }
        public virtual DbSet<students> students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
