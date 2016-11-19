using System;
using System.Collections.Generic;
using GenST2.Models;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        //public IEnumerable<_class> LoadClasses()
        //{
        //    var classes = db.classes.ToList();
        //    return classes;
        //}

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
            check(ddlClasses.SelectedValue);

        }
        public void check(string t)
        {
            string timmy = t;
            lbl_ClassesPrice.Text = timmy;
            //sponse.Write("Passed in this --" + timmy);
        }

        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            //double perHourCharge = 0;


            //if (ddlCourses.SelectedValue.Equals("3"))
            //{
            //    perHourCharge = 18;
            //}
            //else
            //{
            //    perHourCharge = 15;
            //}
            //processCourses(perHourCharge);
        }

        public void processCourses(double perHour)
        {
            if (ddlNumWeeks.SelectedValue == "0")
            {
                LblFees.Text = "";
                double numWeeks;
                numWeeks = Convert.ToDouble(ddlNumWeeks.SelectedValue);
                LblFees.Text = Convert.ToString(numWeeks * perHour);
            }
            else
            {

            }


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
            
            numWeeks = Convert.ToDouble(ddlNumWeeks.SelectedValue);
            //string localck = ddlCourses.SelectedValue;
            LblFees.Text = Convert.ToString(numWeeks * perHourCharge);
        }
    }

}