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
            //string tags = "9,3,12,43,2";
            //List<int> TagIds = tags.Split(',').Select(int.Parse).ToList();


            //string classIds = "";
            //int studentID = 0;
            //foreach (var item in getClassIds())
            //{
            //    classIds += item.classID;
            //    studentID = item.id;
            //    // int id = item.ProductId;
            //    // etc..
            //}

        }

        //public IQueryable<students> getClassIds()
        //{
        //    var ids = (from ck in db.checkins join stu in db.students on 
        //               ck.studentID equals stu.id select new { stu.classID,stu.id }).ToList().
        //               Select(
        //                     tmp => new students()
        //                            {
        //                                 classID = tmp.classID
        //                            });
        //    return ids.AsQueryable();
        //}
        public IQueryable<checkins> lvCheckIn_GetData()
        {
            IQueryable<checkins> query = db.checkins.Where(c => c.currentStudent && c.classDesc != "");
            return query;
        }

        protected void cbPresent_CheckedChanged(object sender, EventArgs e)
        {
            int studentID = Convert.ToInt16(((HiddenField)((CheckBox)sender).Parent.FindControl("studentID")).Value);
            int classID = Convert.ToInt16(((HiddenField)((CheckBox)sender).Parent.FindControl("classID")).Value);
            incrementClasses(studentID,classID);

            //string chkBox = ((Control)sender).UniqueID;
            //Response.Write("<br /><br /><br />"+ studentID); 
        }

        private void incrementClasses( int studentID, int classID)
        {
            ClassInstanceProfile students = new ClassInstanceProfile();

            students.recDate = DateTime.Today;
            var instanceID = (from q in db.classInstanceProfile where 
                             ((q.studentID == studentID) && (q.classID == classID) )
                             select q.id).FirstOrDefault();
            if (instanceID != 0)
            {
                var remaining = (from rem in db.classInstanceProfile where 
                                 (rem.classID == classID) && (rem.remainingInstances != 0) && (rem.id == instanceID)
                                 select rem.remainingInstances).FirstOrDefault();
                var decrement = 1;

                var decrementedInstance = (from s in db.classInstanceProfile where 
                                           ((s.remainingInstances > 0) && (s.classID == classID) 
                                           && (s.id == instanceID)) select s.remainingInstances).FirstOrDefault();

                decrementedInstance = (short)(remaining - decrement);

                var thisRec = (from tr in db.classInstanceProfile where tr.id == instanceID select tr).First();
                thisRec.id = instanceID;
                thisRec.remainingInstances = decrementedInstance;

                db.SaveChanges();
            }
        }
    }
}