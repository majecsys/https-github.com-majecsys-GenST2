﻿using System;
using GenST2.Models;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;
using System.Web.Services;
using System.Web.ModelBinding;

namespace GenST2
{
    public partial class AddNewStudents : System.Web.UI.Page
    {
        public int[] lbClassIDArr { get; set; }
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

        public IQueryable<classes> LoadClasses()
        {
            var query = from b in db.classes
                        select b;
                        return query;
        }

       public IQueryable<REFCourse> LoadCourses()
        {
            var query = from c in db.courses
                        select c;
                        return query;
        }


        public IQueryable<students> getCurrentStudents()
        {
            var query = from s in db.students orderby s.lastname select s;
            return query;
        }

        public IQueryable<students> returnDemo([Control("ddlCurrent")] int? studentID)
        {
            if (studentID != 0)
            {
                hideForms.Visible = false;
            }
            var query = from n in db.students where n.studentID == studentID select n;

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
            //        processClassCourseIDs();
                    insertStudentRec();
              //      checkinUtilities(globalStudentID);
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
                //    lbClassIDArr += (lbcID.Value+",");
                }
            }
            foreach (ListItem lbCourID in lbCourses.Items)
            {
                if (lbCourID.Selected)
                {
                    lbCourseIDs += (lbCourID.Value); // + ",");
                }
            }
            if (lbClassIDArr != null)
            {
            //    lbClassIDArr = lbClassIDArr.Remove(lbClassIDArr.Length - 1);
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
            newStudent.email = email.Value;
            newStudent.phone = phone.Value;
            newStudent.entrydate = DateTime.Today;

            //if (lbClassIDs != null)
            //{
            //    newStudent.classID = lbClassIDs;
            //}
            //else
            //{
            //    newStudent.classID = "0";
            //    lbClassIDs = "0";
            //}
            //if (lbCourseIDs != null)
            //{
            //    newStudent.courseID = lbCourseIDs;
            //}
            //else
            //{
            //    newStudent.courseID = "0";
            //    lbCourseIDs = "0";
            //}

            db.students.Add(newStudent);

            try
            {
                db.SaveChanges();
                //insertIntoClassInstanceProfile(lbClassIDs, newStudent.id);
                //insertIntoCheckin(newStudent.id, lbClassIDs, lbCourseIDs);
                makePurchase(newStudent.studentID);
   //           ;
            }
            catch (Exception)
            {
                db.SaveChanges();
                throw;
            }
        }

        private void makePurchase(int studentID)
        {
            purchases purchase = new purchases();
            foreach (ListItem lbPkgID in lbClasses.Items)
            {
                if (lbPkgID.Selected)
                {
                    purchase.studentID = studentID;
                    purchase.pkgID = Convert.ToInt16((lbPkgID.Value)); ;
                    purchase.purchasedate = DateTime.Today;
                    db.purchases.Add(purchase);
                    try
                    {
                        db.SaveChanges();
                        addFeeAmts(studentID, Convert.ToInt16((lbPkgID.Value)));
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }

            foreach (int item in lbClasses.GetSelectedIndices())
            {
                

            }
           
        }

        //public void insertIntoClassInstanceProfile(string classIDs , int studentID)
        //{
        //    ClassInstanceProfile classInstances = new ClassInstanceProfile();
        //    List<int> Ids = classIDs.Split(',').Select(int.Parse).ToList();

        //    foreach (var cid in Ids)
        //    {
        //        var instance = (from i in db.classes where i.classID == cid select i.instance).FirstOrDefault();
        //        classInstances.classID = cid;
        //        classInstances.studentID = studentID;
        //        classInstances.remainingInstances = instance;
        //        classInstances.recDate = DateTime.Today;
        //        db.classInstanceProfile.Add(classInstances);
        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        } 
        //    }
        //}

        //public void insertIntoCheckin(int id,string classIds,string courseIds)
        //{
        //    checkins checkin = new checkins();

        //    if (classIds != "0")
        //    {
        //        List<int> Ids = classIds.Split(',').Select(int.Parse).ToList();
        //        int courseId = Convert.ToInt16(courseIds);
        //        foreach (var cid in Ids)
        //        {

        //            var classDescription = (from c in db.classes where c.classID == cid select c.description).FirstOrDefault();
        //            var courseDescription = ((from cor in db.courses where cor.courseID == courseId select cor.courseDescription).SingleOrDefault());
        //            if (classDescription != null)
        //            {
        //                checkin.classDesc = classDescription.ToString();
        //            }
        //            else
        //            {
        //                checkin.classDesc = "";
        //            }
        //            if (courseDescription != null)
        //            {
        //                checkin.courseDesc = courseDescription.ToString();
        //            }
        //            else
        //            {
        //                checkin.courseDesc = "";
        //            }
        //            //Convert.ToString(classDescription);
        //            // (Convert.ToString(courseDescription));
        //            checkin.studentID = id;
        //            checkin.FirstName = firstName.Value;
        //            checkin.Lastname = lastname.Value;
        //            checkin.currentStudent = true;
        //            checkin.StartDate = DateTime.Today;
        //            checkin.classID = cid;
        //            db.checkins.Add(checkin);

        //            try
        //            {
        //                db.SaveChanges();

        //            }
        //            catch (Exception)
        //            {
        //                db.SaveChanges();
        //                throw;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        int courseId = Convert.ToInt16(courseIds);
        //        var courseDescription = ((from cor in db.courses where cor.courseID == courseId select cor.courseDescription).SingleOrDefault());
        //        if (courseDescription != null)
        //        {
        //            checkin.courseDesc = courseDescription.ToString();
        //        }
        //        else
        //        {
        //            checkin.courseDesc = "";
        //        }
        //        checkin.studentID = id;
        //        checkin.FirstName = firstName.Value;
        //        checkin.Lastname = lastname.Value;
        //        checkin.currentStudent = true;
        //        checkin.StartDate = DateTime.Today;

        //        db.checkins.Add(checkin);

        //        try
        //        {
        //            db.SaveChanges();

        //        }
        //        catch (Exception)
        //        {
        //            db.SaveChanges();
        //            throw;
        //        }
        //    }
        //}

        protected void addFeeAmts(int studentid,int pkgID)
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
            
            payment.amount = Convert.ToDecimal(fees);
            payment.paymentType = paymentsType();
            payment.studentID = studentid;
            payment.pkgID = pkgID;
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

        //public void checkinUtilities(int studentid)
        //{
        //    students student = new students();
        //    var checkinRec =   (from q in db.students where q.id == studentid select new {q.firstname,q.lastname, q.StartDate, q.classID, q.courseID, q.id }).ToArray();

        //    string cid = "";
        //    string coid = "";
        //    string fn = "";
        //    string ln = "";
        //    int id = 0;

        //    foreach (var item in checkinRec)
        //    {
        //        id = item.id;
        //        ln = item.lastname;
        //        fn = item.firstname;
        //        cid = item.classID;
        //        coid = item.courseID;
        //    }
        //    int[] cidArr = cid.Split(',').Select(int.Parse).ToArray();
        //    int[] coidArr = coid.Split(',').Select(int.Parse).ToArray();
        //    updateClassCourse(cidArr, coidArr,id);
 
        //}

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
        [WebMethod ]
        public static string GetPrices(string selectedID)
        {
            return processPrices(selectedID);
        }

        public static string processPrices(string selectedID)
        {
            ClassCourseElements localDB = new ClassCourseElements();
            int selID = Convert.ToInt16(selectedID);
            int displayAmt = 0;
            var price = (from ca in localDB.classes where (ca.pkgID == selID) select ca.price).SingleOrDefault();
        //    var priceperhr = (from p in localDB.prices join c in localDB.classes on p.pkgID equals c.pkgID where (c.pkgID == selID) select p.priceperhr).SingleOrDefault();

            displayAmt += price; // numclassses * priceperhr;

            return Convert.ToString(displayAmt);
        }

 
    }
}