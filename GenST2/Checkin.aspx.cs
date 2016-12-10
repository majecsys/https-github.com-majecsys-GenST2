using System;
using GenST2.Models;
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
        public IQueryable<Models.checkins> lvCheckIn_GetData()
        {
            var db = new Models.ClassCourseElements();
            IQueryable<checkins> query = db.checkins.Where(c => c.currentStudent) ;
            
            return query;
        }
    }
}