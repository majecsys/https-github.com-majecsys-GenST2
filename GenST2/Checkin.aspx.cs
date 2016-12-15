using System;
using GenST2.Models;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace GenST2
{
    public partial class Checkin : System.Web.UI.Page
    {
        ClassCourseElements db = new ClassCourseElements();
        protected void Page_Load(object sender, EventArgs e)
        {
            //  string ids = getClassIds();
            string tags = "9,3,12,43,2";
            List<int> TagIds = tags.Split(',').Select(int.Parse).ToList();


            string classIds = "";
            int studentID = 0;
            foreach (var item in getClassIds())
            {
                classIds += item.classID;
                studentID = item.id;
                // int id = item.ProductId;
                // etc..
            }

        }

        public IQueryable<students> getClassIds()
        {
            var ids = (from ck in db.checkins join stu in db.students on 
                       ck.studentID equals stu.id select new { stu.classID,stu.id }).ToList().
                       Select(
                             tmp => new students()
                                    {
                                         classID = tmp.classID
                                    });
            return ids.AsQueryable();
        }
        public IQueryable<checkins> lvCheckIn_GetData()
        {
           
            IQueryable<checkins> query = db.checkins.Where(c => c.currentStudent) ;
            
            return query;
        }
    }
}