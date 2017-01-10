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
        }

        public IQueryable<CheckinLVDisplayItems> lvCheckIn_GetData()
        {
            var query = (from c in db.checkins
                         where (c.currentStudent && c.classDesc != "")
                         join cip in db.classInstanceProfile on c.studentID equals cip.studentID

                         into agroup
                         from cip in agroup.DefaultIfEmpty()
                         orderby c.id
                         select new
                         {
                             c.FirstName, 
                             cip.remainingInstances,
                             c.Lastname,
                             c.classDesc,
                             c.courseDesc
                         }).ToList().Select(li => new CheckinLVDisplayItems()
                         {
                             FirstName = li.FirstName,
                             remainingInstances = li.remainingInstances,
                             Lastname = li.Lastname,
                             classDesc = li.classDesc,
                             courseDesc = li.courseDesc
                         });
            return query.AsQueryable();
        }

        protected void cbPresent_CheckedChanged(object sender, EventArgs e)
        {
            int studentID = Convert.ToInt16(((HiddenField)((CheckBox)sender).Parent.FindControl("studentID")).Value);
            int classID = Convert.ToInt16(((HiddenField)((CheckBox)sender).Parent.FindControl("classID")).Value);
            decrementClasses(studentID, classID);

            //string chkBox = ((Control)sender).UniqueID;
            //Response.Write("<br /><br /><br />"+ studentID); 
        }

        private void decrementClasses(int studentID, int classID)
        {
            ClassInstanceProfile students = new ClassInstanceProfile();

            students.recDate = DateTime.Today;
            var instanceID = (from q in db.classInstanceProfile
                              where
                                ((q.studentID == studentID) && (q.classID == classID))
                              select q.id).FirstOrDefault();
            if (instanceID != 0)
            {
                var remaining = (from rem in db.classInstanceProfile
                                 where
                                   (rem.classID == classID) && (rem.remainingInstances != 0) && (rem.id == instanceID)
                                 select rem.remainingInstances).FirstOrDefault();
                var decrement = 1;

                var decrementedInstance = (from s in db.classInstanceProfile
                                           where
                                                 ((s.remainingInstances > 0) && (s.classID == classID) && (s.id == instanceID))
                                           select s.remainingInstances).FirstOrDefault();

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
              //  Label rem = (Label)e.Item.FindControl("remaining");
                //  var remains = (from r in db.classInstanceProfile where r.remainingInstances != 0 select r.remainingInstances).FirstOrDefault();
                // (e.Item.DataItem)
                //    rem.Text =  (from r in db.classInstanceProfile where r.remainingInstances != 0 select r.remainingInstances).FirstOrDefault().ToString();
            }

        }
    }
}