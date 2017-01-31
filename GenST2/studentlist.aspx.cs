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
                         p.studentID
                     }).ToList().Select(li => new StudentDetailsDisplayItems() { classDescription = li.description,
                                                                                 studentID = li.studentID });

            return details.AsQueryable();

            //var som = from c in db.studentDetails where c.studentID == 8 orderby c.classDescription select c ;
            //return som;
        }


        protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                ListView lv = (ListView)e.Item.FindControl("lvStudentDetails");
                Label Lblid = (Label)lv.Controls[0].FindControl("lblTest");

                HiddenField sid = (HiddenField)e.Item.FindControl("selectionKeyForNestedLV");
                Lblid.Text = sid.Value;
            }
        }

        protected void lvStudentDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListView lvDetails = (ListView)e.Item.FindControl("lvStudentDetails");
         
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                //foreach (ListViewItem item in lvDetails.Items)
                //{
                //    CheckBox cbpresent = (CheckBox)item.FindControl("cbPresent");
                //    cbpresent.Checked = true;
                //}
            }
        }

        protected void present_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            ListViewItem lvStudentItem = (ListViewItem)cb.NamingContainer;
            ListViewDataItem dataItem = (ListViewDataItem)lvStudentItem;
            string studentID = lvStudents.DataKeys[dataItem.DisplayIndex].Value.ToString();

            updateAttendanceRec(studentID);
        }

        private void updateAttendanceRec(string studentID)
        {
            int sid = 0;
            sid = Convert.ToInt16(studentID);

            
        }
    }
}