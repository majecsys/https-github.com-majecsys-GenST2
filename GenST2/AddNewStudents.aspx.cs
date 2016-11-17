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

        protected void ddlClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = new DropDownList();
            
            Response.Write(ddl.SelectedValue);

        }
    }

}