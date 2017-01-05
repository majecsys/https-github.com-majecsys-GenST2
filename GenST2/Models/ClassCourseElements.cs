namespace GenST2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;
    using System.Data.Entity.ModelConfiguration.Conventions;
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
        public virtual DbSet <ClassInstanceProfile> classInstanceProfile { get; set; }
        public virtual DbSet<cc> cc { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}
