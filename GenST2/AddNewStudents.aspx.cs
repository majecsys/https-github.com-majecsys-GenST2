using System;
using GenST2.Models;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;
using System.Collections.Generic;

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
   //         processClassFees(lbClasses.SelectedIndex);

        }


        protected void btnSubmitRec_Click(object sender, EventArgs e)
        {
            int localFeeTotal = 0;
            
            if (hiddenTotalFees.Value == "")
            {
                localFeeTotal = int.Parse(HiddenFieldAmtDue.Value);
            }
            else
            {
                localFeeTotal = int.Parse(hiddenTotalFees.Value);
            }
        
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
                    lbCourseIDs += (lbCourID.Value); // + ",");
                }
            }
            if (lbClassIDs != null)
            {
                lbClassIDs = lbClassIDs.Remove(lbClassIDs.Length - 1);
            }
            
            //if (lbCourseIDs != null)
            //{
            //    lbCourseIDs = lbCourseIDs.Remove(lbCourseIDs.Length - 1);
            //}

        }

        public void insertStudentRec()
        {
            students newStudent = new students();

            newStudent.firstname = firstName.Value;
            newStudent.lastname = lastname.Value;
            newStudent.Email =  email.Value;
            newStudent.Phone = phone.Value;
            newStudent.StartDate = DateTime.Today;
            
            if (lbClassIDs != null)
            {
                newStudent.classID = lbClassIDs;
            }
            else
            {
                newStudent.classID = "0";
                lbClassIDs = "0";
            }
            if (lbCourseIDs != null)
            {
                newStudent.courseID = lbCourseIDs;
            }
            else
            {
                newStudent.courseID = "0";
                lbCourseIDs = "0";
            }
            
            db.students.Add(newStudent);

            try
            {
                db.SaveChanges();
                insertIntoClassInstanceProfile(lbClassIDs ,newStudent.id);
                insertIntoCheckin(newStudent.id,lbClassIDs,lbCourseIDs);
                addFeeAmts(newStudent.id);
            }
            catch (Exception)
            {
                db.SaveChanges();
                throw;
            }
        }

        public void insertIntoClassInstanceProfile(string classIDs , int studentID)
        {
            ClassInstanceProfile classInstances = new ClassInstanceProfile();
            List<int> Ids = classIDs.Split(',').Select(int.Parse).ToList();

            foreach (var cid in Ids)
            {
                var instance = (from i in db.classes where i.classID == cid select i.instance).FirstOrDefault();
                classInstances.classID = cid;
                classInstances.studentID = studentID;
                classInstances.remainingInstances = instance;
                classInstances.recDate = DateTime.Today;
                db.classInstanceProfile.Add(classInstances);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                } 
            }
        }

        public void insertIntoCheckin(int id,string classIds,string courseIds)
        {
            checkins checkin = new checkins();

            if (classIds != "0")
            {
                List<int> Ids = classIds.Split(',').Select(int.Parse).ToList();
                int courseId = Convert.ToInt16(courseIds);
                foreach (var cid in Ids)
                {

                    var classDescription = (from c in db.classes where c.classID == cid select c.classDescriptions).FirstOrDefault();
                    var courseDescription = ((from cor in db.courses where cor.courseID == courseId select cor.courseDescription).SingleOrDefault());
                    if (classDescription != null)
                    {
                        checkin.classDesc = classDescription.ToString();
                    }
                    else
                    {
                        checkin.classDesc = "";
                    }
                    if (courseDescription != null)
                    {
                        checkin.courseDesc = courseDescription.ToString();
                    }
                    else
                    {
                        checkin.courseDesc = "";
                    }
                    //Convert.ToString(classDescription);
                    // (Convert.ToString(courseDescription));
                    checkin.studentID = id;
                    checkin.FirstName = firstName.Value;
                    checkin.Lastname = lastname.Value;
                    checkin.currentStudent = true;
                    checkin.StartDate = DateTime.Today;
                    checkin.classID = cid;
                    db.checkins.Add(checkin);

                    try
                    {
                        db.SaveChanges();

                    }
                    catch (Exception)
                    {
                        db.SaveChanges();
                        throw;
                    }
                }
            }
            else
            {
                int courseId = Convert.ToInt16(courseIds);
                var courseDescription = ((from cor in db.courses where cor.courseID == courseId select cor.courseDescription).SingleOrDefault());
                if (courseDescription != null)
                {
                    checkin.courseDesc = courseDescription.ToString();
                }
                else
                {
                    checkin.courseDesc = "";
                }
                checkin.studentID = id;
                checkin.FirstName = firstName.Value;
                checkin.Lastname = lastname.Value;
                checkin.currentStudent = true;
                checkin.StartDate = DateTime.Today;
               
                db.checkins.Add(checkin);

                try
                {
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    db.SaveChanges();
                    throw;
                }
            }
        }

        protected void addFeeAmts(int studentid)
        {
            globalStudentID = studentid;
            int fees = 0;
            //        int classFee = int.Parse(lbl_ClassesPrice.Text);
            if (hiddenTotalFees.Value == "")
            {
              fees   = int.Parse(HiddenFieldAmtDue.Value);
            }
            else
            {
                fees = int.Parse(hiddenTotalFees.Value);
            }
           //(classFee + courseFee);

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