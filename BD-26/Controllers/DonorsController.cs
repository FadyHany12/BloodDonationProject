using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BD_26.Models;

namespace BD_26.Controllers
{
    public class DonorsController : Controller
    {
        private DBModels db = new DBModels();

        // GET: Donors
        public ActionResult Index()
        {
            return View(db.Donors.ToList());
        }

        // GET: Donors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // GET: Donors/Create
        [HttpGet]

        
        
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult Create_ar()
        {
            return View();
        }
        // POST: Donors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "DSsn,Fname,Mname,Lname,email,country,city,sex,birthday,phone,bloodtype,Password", Exclude = "IsEmailVerify,ActivationCode")] Donor donor)
        {
            bool Status = false;
            string Message = "";
            if (ModelState.IsValid)
            {
                #region 
                var isExist = IsEmailExist(donor.email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(donor);
                }
                #endregion

                #region Generate Activation code
                donor.ActivationCode = Guid.NewGuid();
                #endregion


                #region Password Hashing
                donor.Password = Crypto.Hash(donor.Password);
                //donor.ConfirmPassword = Crypto.Hash(donor.ConfirmPassword);
                #endregion


                donor.IsEmailVerify = false;



                db.Donors.Add(donor);
                db.SaveChanges();
                SendVerificationLinkEmail(donor.email, donor.ActivationCode.ToString());
                Message = "Registration successfully done. Account activation link" +
                         "has beem sent to your email :" + donor.email;
                Status = true;

            }
            else
            {
                Message = "Invalid Request";
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(donor);
        }

        public ActionResult Create_ar([Bind(Include = "DSsn,Fname,Mname,Lname,email,country,city,sex,birthday,phone,bloodtype,Password", Exclude = "IsEmailVerify,ActivationCode")] Donor donor)
        {
            bool Status = false;
            string Message = "";
            if (ModelState.IsValid)
            {
                #region 
                var isExist = IsEmailExist(donor.email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(donor);
                }
                #endregion

                #region Generate Activation code
                donor.ActivationCode = Guid.NewGuid();
                #endregion


                #region Password Hashing
                donor.Password = Crypto.Hash(donor.Password);
                //donor.ConfirmPassword = Crypto.Hash(donor.ConfirmPassword);
                #endregion


                donor.IsEmailVerify = false;



                db.Donors.Add(donor);
                db.SaveChanges();
                SendVerificationLinkEmail(donor.email, donor.ActivationCode.ToString());
                Message = "Registration successfully done. Account activation link" +
                         "has beem sent to your email :" + donor.email;
                Status = true;

            }
            else
            {
                Message = "Invalid Request";
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(donor);
        }

        [NonAction]
        public bool IsEmailExist(string emailId)
        {
            var v = db.Donors.Where(a => a.email == emailId).FirstOrDefault();
            return v != null;
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailId, string activationcode)
        {
            var verifyUrl = "/Donors/VerifyAccount/" + activationcode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("201801226@pua.edu.eg", "Dotnet Awesome");
            var toEmail = new MailAddress(emailId);
            var fromEmailPassword = "batterylow61";
            string subject = "Your account is successfully created!";

            string body = "<br/><br/> We are excited to tell you that your Dotnet Awesome account is " +
                "successfully created. Please click on the below link to verify your account" +
                "<br/><br/><a href='" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var Message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(Message);
        }
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            db.Configuration.ValidateOnSaveEnabled = false;
            var v = db.Donors.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();

            if (v != null)
            {
                v.IsEmailVerify = true;
                db.SaveChanges();
                Status = true;
            }
            else
            {
                ViewBag.Message = "Invalid Request";
            }
            ViewBag.Status = Status;
            return View();
        }






        // GET: Donors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // POST: Donors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DSsn,Fname,Mname,Lname,email,country,city,sex,birthday,phone,bloodtype,Password,IsEmailVerify,ActivationCode")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donor);
        }

        // GET: Donors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // POST: Donors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Donor donor = db.Donors.Find(id);
            db.Donors.Remove(donor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
