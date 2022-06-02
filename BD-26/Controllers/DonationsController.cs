using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BD_26.Models;
 //Code for Dontaion
 //step one write a change step two see version control step three   commit 
 //step 1a
namespace BD_26.Controllers
{
    public class DonationsController : Controller
    {
        private DBModels db = new DBModels();
    
        // GET: Donations
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var donations = from s in db.Donations
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    donations = donations.OrderByDescending(s => s.bloodtype);
                    break;
                case "Date":
                    donations = donations.OrderBy(s => s.LastDonationDate);
                    break;
                case "date_desc":
                    donations = donations.OrderByDescending(s => s.LastDonationDate);
                    break;
                default:
                    donations = donations.OrderBy(s => s.bloodtype);
                    break;
            }
            return View(donations.ToList());
        }

        // GET: Donations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // GET: Donations/Create
        public ActionResult Create()
        {
            ViewBag.Donor_Ssn = new SelectList(db.Donors, "DSsn", "Fname");
            return View();
        }


        // GET: Donations/Create
        public ActionResult Create_ar()
        {
            ViewBag.Donor_Ssn = new SelectList(db.Donors, "DSsn", "Fname");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Donation_id,Donor_Ssn,bloodtype,LastDonationDate")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Donations.Add(donation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Donor_Ssn = new SelectList(db.Donors, "DSsn", "Fname", donation.Donor_Ssn);
            return View(donation);
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_ar([Bind(Include = "Donation_id,Donor_Ssn,bloodtype,LastDonationDate")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Donations.Add(donation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Donor_Ssn = new SelectList(db.Donors, "DSsn", "Fname", donation.Donor_Ssn);
            return View(donation);
        }

        // GET: Donations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Donor_Ssn = new SelectList(db.Donors, "DSsn", "Fname", donation.Donor_Ssn);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Donation_id,Donor_Ssn,bloodtype,LastDonationDate")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Donor_Ssn = new SelectList(db.Donors, "DSsn", "Fname", donation.Donor_Ssn);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donation donation = db.Donations.Find(id);
            db.Donations.Remove(donation);
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
