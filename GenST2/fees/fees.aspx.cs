using System;
using GenST2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GenST2
{
    public partial class fees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void processDDL()
        {

        }

        protected void ddlFeeReportTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int daysToSubtract = 0;
            switch (ddlFeeReportTypes.SelectedIndex)
            {
                
                case 0:
                    break;
                case 1:
                    daysToSubtract = 30;
                    selectTotals(daysToSubtract);
                    break;
                case 2:
                    daysToSubtract = 365;
                    selectTotals(daysToSubtract);
                    break;
            }
            //if (ddlFeeReportTypes.SelectedIndex == 0)
            //{

            //    string vop = ddlFeeReportTypes.SelectedValue;
            //   ListItem zip =    ddlFeeReportTypes.SelectedItem;
            //    var questions = "kok";
            //}
        }

        private void selectTotals(int days)
        {
            ClassCourseElements db = new ClassCourseElements();
            var nowDate = DateTime.Today;
            DateTime pastDate = DateTime.Now.AddDays(-days);
            var amt = (from p in db.payments
                       where  ((p.paymentDate >= pastDate) && (p.paymentDate <= nowDate))
                       select  p.amount).Sum() ;
            lblAmts.Visible = true;
            lblAmts.Text ="$" + amt.ToString();
           
        }
    }
}