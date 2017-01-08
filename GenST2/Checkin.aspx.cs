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
        public IQueryable<CheckinLVDisplayItems> lvCheckIn_GetData()
        {
            var query = (from c in db.checkins
                         where (c.currentStudent && c.classDesc != "")
                         join cip in db.classInstanceProfile on c.studentID equals cip.studentID

                         into agroup
                         from cip in agroup.DefaultIfEmpty()

                         select new
                         {
                             c.FirstName,
                             cip.remainingInstances,
                             c.Lastname,
                             c.classDesc,
                             c.courseDesc
                         }).ToList().Select(li => new CheckinLVDisplayItems()
                         {
                              FirstName=li.FirstName,
                              remainingInstances = li.remainingInstances,
                              Lastname = li.Lastname,
                             classDesc = li.classDesc,
                             courseDesc = li.courseDesc
                         });

            //IQueryable<checkins> query =

            //    from c in db.checkins
            //    from cip in db.classInstanceProfile
            //    where c.studentID == cip.studentID
            //    select new checkins()
            //    {
            //        FirstName = c.FirstName,
            //        cip.remainingInstances,
            //        Lastname = c.Lastname,
            //        classDesc = c.classDesc,
            //        courseDesc = c.courseDesc
            //    };


            return query.AsQueryable();
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

        protected void lvCheckIn_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label rem = (Label)e.Item.FindControl("remaining");
              //  var remains = (from r in db.classInstanceProfile where r.remainingInstances != 0 select r.remainingInstances).FirstOrDefault();
              // (e.Item.DataItem)
            //    rem.Text =  (from r in db.classInstanceProfile where r.remainingInstances != 0 select r.remainingInstances).FirstOrDefault().ToString();

            }

        }
    }
}