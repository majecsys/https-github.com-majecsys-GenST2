using System;
using GenST2.Models;
using System.Linq;

namespace GenST2
{
    public partial class AddNewStudents : System.Web.UI.Page
    {
        ClassCourseElements db = new ClassCourseElements();
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.ddlClasses.SelectedIndexChanged += new System.EventHandler(ddlClasses_SelectedIndexChanged);

            if (User.Identity.IsAuthenticated)
            {
                Response.Write("The add page");
            }
            else
            { 
             //   Response.Redirect("/Account/Login.aspx");
            }
        }



        public IQueryable<_class> LoadClasses()
        {
            var query = from b in db.classes
                        select b;
                        return query;
        }

       public IQueryable<course> LoadCourses()
        {
            var query = from c in db.courses
                        select c;
                        return query;
        }

        protected void ddlClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            processClassFees(ddlClasses.SelectedIndex);

        }

        public void processClassFees(int classID)
        {
            if (ddlClasses.SelectedIndex == 1)
            { lbl_ClassesPrice.Text = "72"; }
            if (ddlClasses.SelectedIndex == 2)
            { lbl_ClassesPrice.Text = "136"; }
            if (ddlClasses.SelectedIndex == 3)
            { lbl_ClassesPrice.Text = "192"; }
            if (ddlClasses.SelectedIndex == 4)
            { lbl_ClassesPrice.Text = "228"; }
            if (ddlClasses.SelectedIndex == 5)
            { lbl_ClassesPrice.Text = "20"; }
            if(ddlClasses.SelectedIndex == 7)
            { lbl_ClassesPrice.Text = "100"; }
            if (ddlClasses.SelectedIndex == 8)
            { lbl_ClassesPrice.Text = "30"; }
        }

        protected void ddlNumWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            double numWeeks;
            double perHourCharge = 0;

            if (ddlCourses.SelectedValue.Equals("3"))
            {
                perHourCharge = 18;
            }
            else
            {
                perHourCharge = 15;
            }
            if (ddlCourses.SelectedIndex != 0)
            {
                ddlCourses.BorderColor = System.Drawing.Color.Gainsboro;
                numWeeks = Convert.ToDouble(ddlNumWeeks.SelectedValue);
                LblFees.Text = Convert.ToString(numWeeks * perHourCharge);
            }
            else
            {
                ddlCourses.BorderColor = System.Drawing.Color.Red;
            }
        }
    }

}