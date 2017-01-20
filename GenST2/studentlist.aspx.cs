using System;
using GenST2.Models;
using System.Linq;
using System.Web.ModelBinding;

namespace GenST2
{
    public partial class studentlist : System.Web.UI.Page
    {
        ClassCourseElements db = new ClassCourseElements();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public IQueryable<students> getStudents()
        {
            //var sts = from s in db.students where s.studentID == 21 select s;
            //return sts;
            var query = db.students;
            return query;
        }


        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<StudentDetailsDisplayItems> lvStudentDetails_GetDetails([Control("selectionKeyForNestedLV")] int? studentID)
        {
            var localDB = new ClassCourseElements();
            var v = (from p in localDB.purchases

                     join c in localDB.classes on p.pkgID equals c.pkgID
                     where p.studentID == studentID
                     select new
                     {
                         c.description

                     }).ToList().Select(li => new StudentDetailsDisplayItems() { classDescription = li.description });

            return v.AsQueryable();

            //var som = from c in db.studentDetails where c.studentID == 8 orderby c.classDescription select c ;
            //return som;
        }
    }
}