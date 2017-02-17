using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GenST2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                
            }
            else
            {
                Response.Redirect("/Account/Login.aspx");
            }
            if (HttpContext.Current.User.IsInRole("canView"))
            {
                payments.Visible = true;
            }
        }
    }
}