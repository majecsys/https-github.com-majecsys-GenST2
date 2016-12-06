using System;
using GenST2.Models;
using System.Linq;
using System.Web.UI.WebControls;

namespace GenST2
{
    public partial class AddNewStudents : System.Web.UI.Page
    {
        public string lbClassIDs { get; set; }
        public string lbCourseIDs { get; set; }
      
        ClassCourseElements db = new ClassCourseElements();
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.lbClasses.SelectedIndexChanged += new System.EventHandler(lbClasses_SelectedIndexChanged);

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

        protected void lbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbClasses.BorderColor = System.Drawing.Color.Gainsboro;
            processClassFees(lbClasses.SelectedIndex);

        }

        public void processClassFees(int classID)
        {
            if (lbClasses.SelectedIndex == 1)
            { lbl_ClassesPrice.Text = "72"; }
            if (lbClasses.SelectedIndex == 2)
            { lbl_ClassesPrice.Text = "136"; }
            if (lbClasses.SelectedIndex == 3)
            { lbl_ClassesPrice.Text = "192"; }
            if (lbClasses.SelectedIndex == 4)
            { lbl_ClassesPrice.Text = "228"; }
            if (lbClasses.SelectedIndex == 5)
            { lbl_ClassesPrice.Text = "20"; }
            if(lbClasses.SelectedIndex == 7)
            { lbl_ClassesPrice.Text = "100"; }
            if (lbClasses.SelectedIndex == 8)
            { lbl_ClassesPrice.Text = "30"; }
        }

        protected void ddlNumWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            double numWeeks;
            double perHourCharge = 0;
            //if (lbCourses.SelectedValue.Equals("3"))
            //{
            //    perHourCharge = 18;
            //}
            //else
            //{
            //    perHourCharge = 15;
            //}
            //if (lbCourses.SelectedIndex != 0)
            //{
            //    lbCourses.BorderColor = System.Drawing.Color.Gainsboro;
            //    numWeeks = Convert.ToDouble(ddlNumWeeks.SelectedValue);
            //    lbl_CoursePrice.Text = Convert.ToString(numWeeks * perHourCharge);
            //}
            //else
            //{
            //    lbCourses.BorderColor = System.Drawing.Color.Red;
            //}

            foreach (ListItem courseValue in lbCourses.Items)
            {
                if (courseValue.Selected)
                {
                    lbCourseIDs += courseValue.Value + ",";
                    //if (courseValue.Value.Equals("3"))
                    //{
                    //    perHourCharge = 18;
                    //}
                    //else
                    //{
                    //    perHourCharge = 15;
                    //}
                    //lbCourses.BorderColor = System.Drawing.Color.Gainsboro;
                    //numWeeks = Convert.ToDouble(ddlNumWeeks.SelectedValue);
                    //lbl_CoursePrice.Text = Convert.ToString(numWeeks * perHourCharge);
                }
                else
                {
                    lbCourses.BorderColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnSubmitRec_Click(object sender, EventArgs e)
        {
            if (lbClasses.SelectedIndex !=0 || lbCourses.SelectedIndex != 0)
            {
                processClassCourseIDs();
                insertStudentRec();
            }
            else
            {
                lbCourses.BorderColor = System.Drawing.Color.Red;
                lbClasses.BorderColor = System.Drawing.Color.Red;
            }
            
        }

        private void processClassCourseIDs()
        {
            foreach (ListItem lbcID in lbClasses.Items)
            {
                if (lbcID.Selected)
                {
                    lbClassIDs += (lbcID.Value+",");
                }
            }
            lbClassIDs = lbClassIDs.Remove(lbClassIDs.Length - 1);
            foreach (ListItem lbCourID in lbCourses.Items)
            {
                if (lbCourID.Selected)
                {
                    lbCourseIDs += (lbCourID.Value + ",");
                }
            }
            lbClassIDs = lbClassIDs.Remove(lbClassIDs.Length - 1);
            lbCourseIDs = lbCourseIDs.Remove(lbCourseIDs.Length - 1);
        }

        public void insertStudentRec()
        {
            string className = lbClasses.SelectedItem.ToString();
            string courseName = lbCourses.SelectedItem.ToString();
            students newStudent = new students();
           
            newStudent.firstname = firstName.Value;
            newStudent.lastname = lastname.Value;
            newStudent.Email =  email.Value;
            newStudent.Phone = phone.Value;
            newStudent.StartDate = DateTime.Today;
            newStudent.classID = lbClassIDs; // lbClasses.SelectedIndex;
            newStudent.courseID = lbCourseIDs;
            db.students.Add(newStudent);
            try
            {
                db.SaveChanges();
                addFeeAmts(newStudent.id);
            }
            catch (Exception)
            {
                db.SaveChanges();
                throw;
            }
        }

        protected void addFeeAmts(int studentid)
        {
            if(lbl_ClassesPrice.Text =="")
            {
                lbl_ClassesPrice.Text = "0";
            }
            if (lbl_CoursePrice.Text == "")
            {
                lbl_CoursePrice.Text = "0";
            }

            int classFee = int.Parse(lbl_ClassesPrice.Text);
            int courseFee = int.Parse(lbl_CoursePrice.Text);
            int fees = (classFee + courseFee);
            payments payment = new payments();
            payment.fees = Convert.ToDecimal(fees);
            payment.paymentType = paymentsType();
            payment.studentID = studentid;
            payment.paymentDate = DateTime.Today;
            db.payments.Add(payment);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string paymentsType()
        {
            string payment = "";
            if (cbCash.Checked)
            {
                payment = "Cash";
            }
            if (cbCheck.Checked)
            {
                payment = "Check";
            }
            if (cbCreditCard.Checked)
            {
                payment = "Credit Card";
            }
            return payment;
        }

    }

}