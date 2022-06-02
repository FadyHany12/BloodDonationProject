using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BD_26.Models;

namespace BD_26.Controllers
{
    public class BloodBagsController : Controller
    {
        private DBModels db = new DBModels();

        // GET: BloodBags
        public ActionResult Index()
        {
            var bloodBags = db.BloodBags.Include(b => b.BloodBank).Include(b => b.Donor);
            return View(bloodBags.ToList());
        }

        // GET: BloodBags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodBag bloodBag = db.BloodBags.Find(id);
            if (bloodBag == null)
            {
                return HttpNotFound();
            }
            return View(bloodBag);
        }

        // GET: BloodBags/Create
        public ActionResult Create()
        {
            ViewBag.bank_id = new SelectList(db.BloodBanks, "bank_id", "bank_name");
            ViewBag.DSsn = new SelectList(db.Donors, "DSsn", "Fname");
            return View();
        }

        // POST: BloodBags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BloodBag_id,bloodtype,DSsn,bank_id,BloodBag_Donation,BloodBag_Expiration,Status")] BloodBag bloodBag)
        {
            if (ModelState.IsValid)
            {
                db.BloodBags.Add(bloodBag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bank_id = new SelectList(db.BloodBanks, "bank_id", "bank_name", bloodBag.bank_id);
            ViewBag.DSsn = new SelectList(db.Donors, "DSsn", "Fname", bloodBag.DSsn);
            return View(bloodBag);
        }

        // GET: BloodBags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodBag bloodBag = db.BloodBags.Find(id);
            if (bloodBag == null)
            {
                return HttpNotFound();
            }
            ViewBag.bank_id = new SelectList(db.BloodBanks, "bank_id", "bank_name", bloodBag.bank_id);
            ViewBag.DSsn = new SelectList(db.Donors, "DSsn", "Fname", bloodBag.DSsn);
            return View(bloodBag);
        }

        // POST: BloodBags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BloodBag_id,bloodtype,DSsn,bank_id,BloodBag_Donation,BloodBag_Expiration,Status")] BloodBag bloodBag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloodBag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bank_id = new SelectList(db.BloodBanks, "bank_id", "bank_name", bloodBag.bank_id);
            ViewBag.DSsn = new SelectList(db.Donors, "DSsn", "Fname", bloodBag.DSsn);
            return View(bloodBag);
        }

        // GET: BloodBags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodBag bloodBag = db.BloodBags.Find(id);
            if (bloodBag == null)
            {
                return HttpNotFound();
            }
            return View(bloodBag);
        }

        // POST: BloodBags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BloodBag bloodBag = db.BloodBags.Find(id);
            db.BloodBags.Remove(bloodBag);
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
