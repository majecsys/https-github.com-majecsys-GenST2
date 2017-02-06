using System;
using GenST2.Models;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

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
            var v = (from s in db.students
                     join p in db.purchases on s.studentID equals p.studentID
                     join c in db.classes on p.pkgID equals c.pkgID
                     select new
                     {
                         s.firstname,
                         s.lastname,
                         s.studentID

                     }).Distinct().ToList().Select(n => new students() { firstname = n.firstname,
                                                              lastname = n.lastname, studentID = n.studentID });
            return v.AsQueryable();
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
            
            var details = (from p in localDB.purchases

                     join c in localDB.classes on p.pkgID equals c.pkgID
                     where p.studentID == studentID
                     select new
                     {
                         c.description,
                         p.studentID,
                         p.pkgID
                     }).ToList().Select(li => new StudentDetailsDisplayItems() { classDescription = li.description,
                                                                                 studentID = li.studentID, pkgID = li.pkgID });
            
            return details.AsQueryable();

            //var som = from c in db.studentDetails where c.studentID == 8 orderby c.classDescription select c ;
            //return som;
        }


        protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                //var warn = (from f in db.purchases where f.warningFlag == true select f).ToList();
                            
                //ListView lvDetails = (ListView)e.Item.FindControl("lvStudentDetails");

                //foreach (ListViewItem item in lvDetails.Items)
                //{
                   
                //    foreach (var flag in warn.Where(w => w.warningFlag == true))
                //    {
                //        Label lbl = (Label)item.FindControl("lblClassDesc");
                //        lbl.BackColor = System.Drawing.Color.Pink;

                //        break;
                //    }
                //    break;
                //}
            }
        }

        protected void lvStudentDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //      Label ll = (Label)e Item.FindControl("lblTest");
            //ListView lv = (ListView)sender;
            //ListViewItem lvStudentItem = (ListViewItem)lv.NamingContainer;

            //ListView lvDetails;
            //lvDetails = (ListView)lv.FindControl("lvStudentDetails");
            //var warn = (from f in db.purchases where f.warningFlag == true select f).First();
            //if (lvDetails != null)
            //{
            //    if (e.Item.ItemType == ListViewItemType.DataItem)
            //    {

            //    lvDetails = (ListView)e.Item.FindControl("lvStudentDetails");
            //    foreach (ListViewItem item in lvDetails.Items)
            //        {
            //            if (warn.warningFlag == true)
            //            {
            //                CheckBox cbpresent = (CheckBox)item.FindControl("cbPresent");
            //                cbpresent.BackColor = System.Drawing.Color.Pink;
            //            }

            //        }
            //    }
            //}

        }


        protected void present_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            //ListViewItem lvStudentItem = (ListViewItem)cb.NamingContainer;
            //ListViewDataItem dataItem = (ListViewDataItem)lvStudentItem;
            //string studentID = lvStudents.DataKeys[dataItem.DataItemIndex].Values[0].ToString();

            int hPkgID = Convert.ToInt16(((HiddenField)((CheckBox)sender).Parent.FindControl("hiddenPkgID")).Value);
            string studentID = ((HiddenField)((CheckBox)sender).Parent.FindControl("hiddenStudentID")).Value;
            Label lbl = (Label)((CheckBox)sender).Parent.FindControl("lblClassDesc");
            updateAttendanceRec(hPkgID, studentID,lbl);
            cb.Enabled = false;
        }

        private void updateAttendanceRec(int hPkgID, string studentID,Label lbl)
        {
            ClassCourseElements locDB = new ClassCourseElements();

            
            int classesBalance = 0;
            var decrement = 1;
            int sid = 0;

            sid = Convert.ToInt16(studentID);
            int pkgID = Convert.ToInt16(hPkgID);

            int dropIncheck = pkgID;

            var remainingClasses = (from p in locDB.purchases where (p.studentID == sid) && (p.pkgID == pkgID) select p).First();
            if (remainingClasses.numclasses == 2)
            {
                lbl.BackColor = System.Drawing.Color.Pink;
            }
            if (remainingClasses.numclasses < 1)
            {
                var delRec = (from d in locDB.purchases where (d.studentID == sid) && (d.pkgID == pkgID) select d).First();
                locDB.purchases.Remove(delRec);
                try
                {
                    locDB.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                classesBalance = (short)(remainingClasses.numclasses - decrement);
                remainingClasses.numclasses = classesBalance;
                if (dropIncheck != 1)
                {
                    try
                    {
                        locDB.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            
        }


    }
}