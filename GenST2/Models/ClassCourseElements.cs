namespace GenST2.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    public partial class ClassCourseElements : DbContext
    {
        //internal readonly object checkin;

        public ClassCourseElements()
            : base("name=dbCourseClass")
        {
        }

     
        public virtual DbSet<classcard> classcard { get; set; }
        public virtual DbSet<courses> courses { get; set; }
        public virtual DbSet<payments> payments { get; set; }
        public virtual DbSet<students> students { get; set; }
        public virtual DbSet<checkins> checkins { get; set; }
        public virtual DbSet <classCourse> classCourse { get; set; }
        public virtual DbSet<purchases> purchases { get; set; }
        public virtual DbSet<semesterCourses> semesterCourses { get; set; }
        public virtual DbSet <ClassInstanceProfile> classInstanceProfile { get; set; }
        public virtual DbSet <StudentDetailsDisplayItems> studentDetails{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}
