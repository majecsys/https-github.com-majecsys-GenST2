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

                     join c in localDB.classcard on p.classcardID equals c.classcardID
                                where ( (p.studentID == studentID) && (p.numclasses != 0) )
                     select new
                     {
                         c.description,
                         p.studentID,
                         p.classcardID,
                         p.classexpiration,
                         p.purchaseID
                     }).ToList().Select(li => new StudentDetailsDisplayItems() { classDescription = li.description, classexpiration = li.classexpiration,
                                                                                 studentID = li.studentID, classcardID = li.classcardID,purchaseID = li.purchaseID
                     });
            var courseDetails= (from p in localDB.purchases

                                join co in localDB.courses on p.courseID equals co.courseID
                                where p.studentID == studentID
                                select new
                                {
                                    co.name,
                                    p.studentID,
                                    p.classcardID,
                                    p.expirationdate,
                                    p.classexpiration
                                    ,p.purchaseID
                                }).ToList().Select(sub => new StudentDetailsDisplayItems()
                                {
                                    classexpiration = sub.classexpiration,
                                    expiration = sub.expirationdate,
                                    name = sub.name,
                                    studentID = sub.studentID,
                                    classcardID = sub.classcardID,
                                    purchaseID = sub.purchaseID
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
        private int getNumLeft(int purchaseID)
        {
            var numLeft = (from r in db.purchases where (r.purchaseID == purchaseID)  orderby r.studentID select r.numclasses).FirstOrDefault();
            return numLeft;
        }
        protected void lvStudentDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //Response.Write("<br/><br/><br/>--------"+e.Item.UniqueID);
            StudentDetailsDisplayItems dataItem = (StudentDetailsDisplayItems)e.Item.DataItem;

            Label lblCardDesc = (Label)e.Item.FindControl("lblCardDesc");
            int cntLeft = getNumLeft(dataItem.purchaseID);
     //       var numLeft = (from r in db.purchases where (r.purchaseID == dataItem.purchaseID) && (r.numclasses <= 2) orderby r.studentID select r).First();
            if (cntLeft <= 2 && cntLeft != 0)
            {
                lblCardDesc.BackColor = System.Drawing.Color.Pink;
            }
            //if (numLeft.numclasses <= 2)
            //{
            //       lblCardDesc.BackColor = System.Drawing.Color.Pink;
            //}

            //foreach (var item in numLeft)
            //{
            //    //       Response.Write("<br/>values of item ------ " + item.numclasses + "  " + item.studentID + "<br/>");

            //    if ((item.numclasses <= 2) && (item.studentID == dataItem.studentID))
            //    {
            //        lblCardDesc.BackColor = System.Drawing.Color.Pink;
            //    }
            //    else
            //    {
            //        lblCardDesc.BackColor = System.Drawing.Color.White;
            //    }
            //    //    break;
            //}


            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblexp = (Label)e.Item.FindControl("lblExpiration");

                if (DateTime.Today.AddDays(7) >= dataItem.expiration)
                {
                    lblexp.Style.Add("color", "#FF0000");
                }
                else
                {
                }
                CheckBox cbpresent = (CheckBox)e.Item.FindControl("cbPresent");
                HtmlTableRow coursenamerow = (HtmlTableRow)e.Item.FindControl("courseNameRow");
                
                
                if (lblCardDesc.Text == "")
                {
                    cbpresent.Visible = false;
                }
                Label lbCourseName = (Label)e.Item.FindControl("lblCourseName");
                if (lbCourseName.Text != "")
                {
                    lblexp.Text = dataItem.expiration.ToString("d");
                    //if (getExpirationDate(dataitem.expiration))
                    //{
                    //}
                    //else
                    //{
                    //}
                }
                else
                {
                    coursenamerow.Visible = false;
                }

                Label lblClassExpiration = (Label)e.Item.FindControl("classExpiration");
                lblClassExpiration.Text = dataItem.classexpiration.ToString("d");
                if (lbCourseName.Text != "")
                {
                    
                    lblClassExpiration.Visible = false;
                }
            }
        }

        //public bool  getExpirationDate(DateTime expiration)
        //{
        //    bool warn = false;
        //    var nowDate = DateTime.Today;
        //    DateTime pastDate = DateTime.Now.AddDays(-7);

        //    return warn;
        //}

        protected void present_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            //ListViewItem lvStudentItem = (ListViewItem)cb.NamingContainer;
            //ListViewDataItem dataItem = (ListViewDataItem)lvStudentItem;
            //string studentID = lvStudents.DataKeys[dataItem.DataItemIndex].Values[0].ToString();

            int hPkgID = Convert.ToInt16(((HiddenField)((CheckBox)sender).Parent.FindControl("hiddenPkgID")).Value);
            string studentID = ((HiddenField)((CheckBox)sender).Parent.FindControl("hiddenStudentID")).Value;
            Label lblcardDesc = (Label)((CheckBox)sender).Parent.FindControl("lblCardDesc");
            updateAttendanceRec(hPkgID, studentID,lblcardDesc);
            cb.Enabled = false;
        }

        private void updateAttendanceRec(int hPkgID, string studentID,Label lblcardDesc)
        {
            ClassCourseElements locDB = new ClassCourseElements();

            
            int classesBalance = 0;
            var decrement = 1;
            int sid = 0;

            sid = Convert.ToInt16(studentID);
            int classID = Convert.ToInt16(hPkgID);

           // int dropIncheck = classID;

            var remainingClasses = (from p in locDB.purchases where (p.studentID == sid) && (p.classcardID == classID) select p).First();
            if (remainingClasses.numclasses == 2)
            {
                lblcardDesc.BackColor = System.Drawing.Color.Pink;
            }
            if (remainingClasses.numclasses < 1)
            {
                var delRec = (from d in locDB.purchases where (d.studentID == sid) && (d.classcardID == classID) select d).First();
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
                //if (dropIncheck != 1)
                //{
                    try
                    {
                        locDB.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
              //  }
            }
            
        }

        //protected void lvStudentDetails_ItemCreated(object sender, ListViewItemEventArgs e)
        //{
        //    StudentDetailsDisplayItems dataItem = (StudentDetailsDisplayItems)e.Item.DataItem;
        //    Label lblCardDesc = (Label)e.Item.FindControl("lblCardDesc");

        //    int cntLeft = getNumLeft(dataItem.purchaseID);
        //    if (cntLeft <= 2 && cntLeft != 0)
        //    {
        //           lblCardDesc.BackColor = System.Drawing.Color.Pink;
        //    }
        //}


    }
}