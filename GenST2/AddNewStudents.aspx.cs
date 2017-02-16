using System;
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

        public IQueryable<classcard> LoadClasses()
        {
            var query = from b in db.classcard
                        select b;
                        return query;
        }

       public IQueryable<courses> LoadCourses()
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

        protected void lbClassCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbClassCard.BorderColor = System.Drawing.Color.Gainsboro;
            //         processClassFees(lbClasses.SelectedIndex);
        }

        protected void ddlCurrent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["currentStudentID"] = null;
            DropDownList ddl = (DropDownList)sender;
            ViewState["currentStudentID"] = Convert.ToInt16(ddl.SelectedValue);
        }

        protected void btnSubmitRec_Click(object sender, EventArgs e)
        {
            Convert.ToInt16(ViewState["currentStudentID"]);
            int localFeeTotal = 0;
            
            if (hiddenTotalFees.Value == "")
            {
                localFeeTotal = int.Parse(HiddenFieldAmtDue.Value);
            }
            else
            {
                localFeeTotal = int.Parse(hiddenTotalFees.Value);
            }
            //int classSel = lbClasses.SelectedIndex;
            //int courSel = lbCourses.SelectedIndex;
            if (lbClassCard.SelectedIndex >= 0 || lbCourses.SelectedIndex >= 0)
            {
                if (requirePaymentType())
                {
                    //        processClassCourseIDs();
                    if (ViewState["currentStudentID"] == null)
                    {
                        int currID = 0;
                        insertStudentRec(currID);
                    }
                    else
                    {
                        int currID = Convert.ToInt16(ViewState["currentStudentID"]);
                        insertStudentRec(currID);
                    }
                   // insertStudentRec();
              
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
                lbClassCard.BorderColor = System.Drawing.Color.Red;
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
            foreach (ListItem lbcID in lbClassCard.Items)
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

        public void insertStudentRec(int currID)
        {
           
            students newStudent = new students();

            newStudent.firstname = firstName.Value;
            newStudent.lastname = lastname.Value;
            newStudent.email = email.Value;
            newStudent.phone = phone.Value;
            newStudent.entrydate = DateTime.Today;
            db.students.Add(newStudent);
            if (currID == 0)
            {
                try
                {
                    db.SaveChanges();
                    addFeeAmts(newStudent.studentID);
                    makePurchase(newStudent.studentID);                  
                }
                catch (Exception)
                {
                    db.SaveChanges();
                    throw;
                }
            }
            else
            {
                try
                {
                    db.SaveChanges();
                    //insertIntoClassInstanceProfile(lbClassIDs, newStudent.id);
                    //insertIntoCheckin(newStudent.id, lbClassIDs, lbCourseIDs);
                    makePurchase(currID);
                    //           ;
                }
                catch (Exception)
                {
                   // db.SaveChanges();
                    throw;
                }
            }

        }
        
        private void makePurchase(int studentID)
        {
            purchases purchaseClass = new purchases();
            purchases purchaseCourse = new purchases();
            
            foreach (ListItem lbPkgID in lbClassCard.Items)
            {
                if (lbPkgID.Selected)
                {
                    int lbValue = Convert.ToInt16(lbPkgID.Value);
                    var classesLength = (from c in db.classcard
                                                        where c.classcardID == lbValue
                                                        orderby c.classcardID
                                                        select new { c.cardLength, c.numclasses }).First(); // loadNumClasses(lbValue); 

                    purchaseClass.classexpiration = DateTime.Now.AddDays(classesLength.cardLength); 
                    purchaseClass.numclasses = classesLength.numclasses;
                    purchaseClass.studentID = studentID;
                    purchaseClass.classcardID = Convert.ToInt16((lbPkgID.Value)); ;
                    purchaseClass.purchasedate = DateTime.Today;
                    db.purchases.Add(purchaseClass);
                    try
                    {
                        db.SaveChanges();
                       // addFeeAmts(studentID);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            if (hiddenValueNumWeeks.Value != "")
            {
                int numWeeks = int.Parse(hiddenValueNumWeeks.Value);
                double numDaysInWeeks = numWeeks * 7;
                foreach (ListItem lbCourseID in lbCourses.Items)
                {
                    if (lbCourseID.Selected)
                    {
                        int lbCourseValue = Convert.ToInt16(lbCourseID.Value);
                        purchaseCourse.numweeks = int.Parse(hiddenValueNumWeeks.Value);
                        purchaseCourse.expirationdate = DateTime.Now.AddDays(numDaysInWeeks);
                        purchaseCourse.studentID = studentID;
                        purchaseCourse.courseID = Convert.ToInt16(lbCourseID.Value);
                        purchaseCourse.purchasedate = DateTime.Today;
                        db.purchases.Add(purchaseCourse);
                        try
                        {
                            db.SaveChanges();
                         //   addFeeAmts(studentID);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }


        }

        //private int loadNumWeeks(int lbCourseValue)
        //{
        //    ClassCourseElements locDb = new ClassCourseElements();
        //    var numweeks = (from c in locDb.courses
        //                      join p in locDb.purchases on c.courseID equals p.courseID
        //                      where c.courseID == lbCourseValue
        //                      select c.numweeks).First();
        //    return numweeks;
        //}

        private int loadNumClasses(int lbValue)
        {
            ClassCourseElements locDb = new ClassCourseElements();
            var numclasses = (from num in locDb.classcard
                              join p in locDb.purchases on num.classcardID equals p.classcardID
                              where num.classcardID == lbValue
                              select num.numclasses).FirstOrDefault();
          return numclasses;
        }


        protected void addFeeAmts(int studentid)
        {
           
            int fees = 0;
            if (hiddenTotalFees.Value == "")
            {
              fees   = int.Parse(HiddenFieldAmtDue.Value);
            }
            else
            {
                fees = int.Parse(hiddenTotalFees.Value);
            }

            payments payment = new payments();

            payment.amount = fees; // Convert.ToDecimal(fees);
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
            var price = (from ca in localDB.classcard where (ca.classcardID == selID) select ca.price).SingleOrDefault();
        //    var priceperhr = (from p in localDB.prices join c in localDB.classes on p.classID equals c.classID where (c.classID == selID) select p.priceperhr).SingleOrDefault();

            displayAmt += price; // numclassses * priceperhr;

            return Convert.ToString(displayAmt);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void currentStudentDemo_UpdateItem(int studentID)
        {
            ClassCourseElements localDB = new ClassCourseElements();
            GenST2.Models.students item = localDB.students.Find(studentID);

            if (item == null)
            { 
                // The item wasn't found 
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", studentID));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                localDB.SaveChanges();
            }
        }


    }
}