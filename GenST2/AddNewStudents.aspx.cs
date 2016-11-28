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
            ddlClasses.BorderColor = System.Drawing.Color.Gainsboro;
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
                lbl_CoursePrice.Text = Convert.ToString(numWeeks * perHourCharge);
            }
            else
            {
                ddlCourses.BorderColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSubmitRec_Click(object sender, EventArgs e)
        {
            if (ddlClasses.SelectedIndex !=0 || ddlCourses.SelectedIndex != 0)
            {
                insertStudentRec();
            }
            else
            {
                ddlCourses.BorderColor = System.Drawing.Color.Red;
                ddlClasses.BorderColor = System.Drawing.Color.Red;
            }
            
        }

        public void insertStudentRec()
        {
            string className = ddlClasses.SelectedItem.ToString();
            string courseName = ddlCourses.SelectedItem.ToString();
            students newStudent = new students();
           
            newStudent.firstname = firstName.Value;
            newStudent.lastname = lastname.Value;
            newStudent.Email =  email.Value;
            newStudent.Phone = phone.Value;
            newStudent.StartDate = DateTime.Today;
            newStudent.classID = ddlClasses.SelectedIndex;
            newStudent.courseID = ddlCourses.SelectedIndex;
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