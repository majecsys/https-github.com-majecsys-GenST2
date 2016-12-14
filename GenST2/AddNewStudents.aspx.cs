using System;
using GenST2.Models;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;

namespace GenST2
{
    public partial class AddNewStudents : System.Web.UI.Page
    {
        public string lbClassIDs { get; set; }
        public string lbCourseIDs { get; set; }
        public int globalStudentID { get; set; }

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


        protected void btnSubmitRec_Click(object sender, EventArgs e)
        {
            int localFeeTotal = int.Parse(hiddenTotalFees.Value);

            if (lbClasses.SelectedIndex !=0 || lbCourses.SelectedIndex != 0)
            {
                if (requirePaymentType())
                {
                    processClassCourseIDs();
                    insertStudentRec();
                    checkinUtilities(globalStudentID);
                    Response.Redirect("default.aspx");

                }
                else
                {
                   lbl_totalFees.Text = Convert.ToString(localFeeTotal); ;
                    paidByRow.Style["border-style"] = "dotted";
                    //     paidByRow.Attributes.Add("style", "border-color:#ff0000");
                }
            }
            else
            {
                lbCourses.BorderColor = System.Drawing.Color.Red;
                lbClasses.BorderColor = System.Drawing.Color.Red;
            }
        }

        public bool requirePaymentType()
        {
            bool isChecked=false;

            if (cbCash.Checked)
            {
                isChecked = true;
            }
            else if (cbCheck.Checked)
            {
                isChecked = true;
            }
            else if (cbCreditCard.Checked)
            {
                isChecked = true;
            }
            return isChecked;
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
            globalStudentID = studentid;

    //        int classFee = int.Parse(lbl_ClassesPrice.Text);
            int fees = int.Parse(hiddenTotalFees.Value); //(classFee + courseFee);

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

        public void checkinUtilities(int studentid)
        {
            students student = new students();
            var checkinRec =   (from q in db.students where q.id == studentid select new {q.firstname,q.lastname, q.StartDate, q.classID, q.courseID, q.id }).ToArray();

            string cid = "";
            string coid = "";
            string fn = "";
            string ln = "";
            int id = 0;

            foreach (var item in checkinRec)
            {
                id = item.id;
                ln = item.lastname;
                fn = item.firstname;
                cid = item.classID;
                coid = item.courseID;
            }
            int[] cidArr = cid.Split(',').Select(int.Parse).ToArray();
            int[] coidArr = coid.Split(',').Select(int.Parse).ToArray();
            updateClassCourse(cidArr, coidArr,id);
 
        }

        private void updateClassCourse(int[] cidArr, int[] coidArr,int studentID)
        {
            classCourse classCourseElements = new classCourse();
            int len = cidArr.Length;

            for (int i = 0; i < Math.Max(cidArr.Length, coidArr.Length); i++)
            {
                classCourseElements.cid = cidArr.ElementAtOrDefault(i);
                classCourseElements.coid = coidArr.ElementAtOrDefault(i);
                classCourseElements.sid = studentID;
                db.classCourse.Add(classCourseElements);

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    db.SaveChanges();
                }
            }
            //foreach ( var ids in coidArr.Zip(cidArr, Tuple.Create))
            ////foreach (var ids in cidArr)
            //{
            //    classCourseElements.cid = ids.Item2;
            //    classCourseElements.coid = ids.Item1;
            //    classCourseElements.sid = studentID;

            //    db.classCourse.Add(classCourseElements);

            //    try
            //    {
            //        db.SaveChanges();
            //    }
            //    catch (DbEntityValidationException ex)
            //    {
            //        db.SaveChanges();
            //    }
            //}



            //   throw new NotImplementedException();

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