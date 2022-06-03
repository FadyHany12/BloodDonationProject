using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BD_26.Models;

namespace BD_26.Controllers
{
    public class Patient_RequestsController : Controller
    {
        private DBModels db = new DBModels();
        [Authorize]
        // GET: Patient_Requests
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
            ViewBag.NameSortParm1 = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var patientRequests = from s in db.Patient_Requests
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                patientRequests = patientRequests.Where(s => s.Patient_name.ToUpper().Contains(searchString.ToUpper())
                                       || s.bloodtype.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    patientRequests = patientRequests.OrderByDescending(s => s.Patient_name);
                    break;
                case "name_asc":
                    patientRequests = patientRequests.OrderBy(s => s.Patient_name);
                    break;

                default:
                    patientRequests = patientRequests.OrderBy(s => s.bloodtype);
                    break;
            }
            return View(patientRequests.ToList());
        }

        // GET: Patient_Requests/Details/5
        //[Authorize(Roles ="Donor")]
        //[Route("https://www.google.com/")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient_Requests patient_Requests = db.Patient_Requests.Find(id);
            if (patient_Requests == null)
            {
                return HttpNotFound();
            }
            return View(patient_Requests);
        }

        // GET: Patient_Requests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient_Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient_Requests patient_Requests)
        {
            string fileName = Path.GetFileNameWithoutExtension(patient_Requests.Imagefile.FileName);
            string extension = Path.GetExtension(patient_Requests.Imagefile.FileName);
            fileName = fileName + DateTime.Now.ToString("yy-MM-dd") + extension;
            patient_Requests.permission_path = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            patient_Requests.Imagefile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.Patient_Requests.Add(patient_Requests);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient_Requests);
        }

        // GET: Patient_Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient_Requests patient_Requests = db.Patient_Requests.Find(id);
            if (patient_Requests == null)
            {
                return HttpNotFound();
            }
            return View(patient_Requests);
        }

        // POST: Patient_Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Patient_Ssn,Patient_name,bloodtype,permission_ID,permission_path,Status")] Patient_Requests patient_Requests)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient_Requests).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient_Requests);
        }

        // GET: Patient_Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient_Requests patient_Requests = db.Patient_Requests.Find(id);
            if (patient_Requests == null)
            {
                return HttpNotFound();
            }
            return View(patient_Requests);
        }

        // POST: Patient_Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient_Requests patient_Requests = db.Patient_Requests.Find(id);
            db.Patient_Requests.Remove(patient_Requests);
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
