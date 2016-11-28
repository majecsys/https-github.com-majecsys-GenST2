using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GenST2
{
    public partial class Checkin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public IQueryable<Models.checkin> lvCheckIn_GetData()
        {
            var db = new Models.ClassCourseElements();
            IQueryable<Models.checkin> query = db.checkin.Where(c => c.currentStudent) ;
            return query;
        }
    }
}