using System;
using GenST2.Models;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace GenST2
{
    public partial class Checkin : System.Web.UI.Page
    {
        ClassCourseElements db = new ClassCourseElements();
        protected void Page_Load(object sender, EventArgs e)
        {
            //  string ids = getClassIds();
            string tags = "9,3,12,43,2";
            List<int> TagIds = tags.Split(',').Select(int.Parse).ToList();


            string classIds = "";
            int studentID = 0;
            foreach (var item in getClassIds())
            {
                classIds += item.classID;
                studentID = item.id;
                // int id = item.ProductId;
                // etc..
            }

        }

        public IQueryable<students> getClassIds()
        {
            var ids = (from ck in db.checkins join stu in db.students on 
                       ck.studentID equals stu.id select new { stu.classID,stu.id }).ToList().
                       Select(
                             tmp => new students()
                                    {
                                         classID = tmp.classID
                                    });
            return ids.AsQueryable();
        }
        public IQueryable<checkins> lvCheckIn_GetData()
        {
           
            IQueryable<checkins> query = db.checkins.Where(c => c.currentStudent && c.classDesc != "");
            ;
            return query;
        }
        
        public void processCbCheck(string cb)
        {

        }
        protected void cbPresent_CheckedChanged(object sender, EventArgs e)
        {
            int studentID = Convert.ToInt16(((HiddenField)((CheckBox)sender).Parent.FindControl("studentID")).Value);
            incrementClasses(studentID);

            //string chkBox = ((Control)sender).UniqueID;
            //Response.Write("<br /><br /><br />"+ studentID); 
        }

        private void incrementClasses( int studentID)
        {
            students students = new students();
            var query = from q in db.students where ((q.id == studentID) && (q.remainingClasses != 0)) select new { q.id, q.firstname };
            if (query != null)
            {
                var remaining = (from rem in db.students where rem.remainingClasses != 0 select rem.remainingClasses).FirstOrDefault();
                var decrement = 1;

                students stu = (from s in db.students where (s.remainingClasses > 0) select s).FirstOrDefault();
                stu.remainingClasses = (short)(remaining - decrement);

                db.SaveChanges();
            }
        //    query.Dump();
           
        }
    }
}