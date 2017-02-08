using System;
using GenST2.Models;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

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
                 //    join c in db.classes on p.classID equals c.classID
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
            
            var classDetails = (from p in localDB.purchases

                     join c in localDB.classes on p.classID equals c.classID
                     where p.studentID == studentID
                     select new
                     {
                         c.description,
                         p.studentID,
                         p.classID
                     }).ToList().Select(li => new StudentDetailsDisplayItems() { classDescription = li.description,
                                                                                 studentID = li.studentID, classID = li.classID });
            var courseDetails= (from p in localDB.purchases

                                join co in localDB.courses on p.courseID equals co.courseID
                                where p.studentID == studentID
                                select new
                                {
                                    co.name,
                                    p.studentID,
                                    p.classID,
                                    p.expirationdate
                                }).ToList().Select(sub => new StudentDetailsDisplayItems()
                                {
                                    expiration = sub.expirationdate,
                                    name = sub.name,
                                    studentID = sub.studentID,
                                    classID = sub.classID
                                });
            var combo = classDetails.Union(courseDetails);

            return combo.AsQueryable();

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
            //Label ll = (Label)e Item.FindControl("lblTest");
            //ListView lv = (ListView)sender;
            //ListViewItem lvStudentItem = (ListViewItem)lv.NamingContainer;



            //           var warn = (from f in db.purchases where f.warningFlag == true select f).First();

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
              //  ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                StudentDetailsDisplayItems dataitem = (StudentDetailsDisplayItems)e.Item.DataItem;

               // dataitem.expiration = DateTime.Now;

                CheckBox cbpresent = (CheckBox)e.Item.FindControl("cbPresent");
                HtmlTableRow coursenamerow = (HtmlTableRow)e.Item.FindControl("courseNameRow");
                Label lblexp = (Label)e.Item.FindControl("lblExpiration");
                Label lbclassdec = (Label)e.Item.FindControl("lblClassDesc");
                if (lbclassdec.Text == "")
                {
                    // getExpirationDate(dataitem.expiration, dataitem.studentID);
                    
                    cbpresent.Visible = false;
                }
                Label lbCourseName = (Label)e.Item.FindControl("lblCourseName");
                if (lbCourseName.Text != "")
                {
                    lblexp.Text = Convert.ToString(dataitem.expiration);
                }
                else
                {
                    coursenamerow.Visible = false;
                }




                //lvDetails = (ListView)e.Item.FindControl("lvStudentDetails");
                //foreach (ListViewItem item in lvDetails.Items)
                //{
                //    if (warn.warningFlag == true)
                //    {
                //        CheckBox cbpresent = (CheckBox)item.FindControl("cbPresent");
                //        cbpresent.BackColor = System.Drawing.Color.Pink;
                //    }

                //}
            }


        }

        private DateTime getExpirationDate(DateTime expiration, int studentID)
        {
            throw new NotImplementedException();
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
            int classID = Convert.ToInt16(hPkgID);

            int dropIncheck = classID;

            var remainingClasses = (from p in locDB.purchases where (p.studentID == sid) && (p.classID == classID) select p).First();
            if (remainingClasses.numclasses == 2)
            {
                lbl.BackColor = System.Drawing.Color.Pink;
            }
            if (remainingClasses.numclasses < 1)
            {
                var delRec = (from d in locDB.purchases where (d.studentID == sid) && (d.classID == classID) select d).First();
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