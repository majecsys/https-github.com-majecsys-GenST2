using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GenST2
{
    public partial class AddNewStudents : System.Web.UI.Page
    {
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
    }
}