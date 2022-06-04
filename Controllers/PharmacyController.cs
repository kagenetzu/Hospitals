using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KURSLAB3.Models;

namespace KURSLAB3.Controllers
{
    public class PharmacyController : Controller
    {
        private MedicineEntities db = new MedicineEntities();

        // GET: Pharmacy
        public ActionResult Index(int pg = 1)
        {
            //return View(db.Pharmacy.ToList());
            List<Pharmacy> pharmacys = db.Pharmacy.ToList();
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;

            int rescCount = pharmacys.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = pharmacys.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: Pharmacy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacy pharmacy = db.Pharmacy.Find(id);
            if (pharmacy == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy);
        }

        // GET: Pharmacy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pharmacy/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pharmacyNumber,pharmacyAddress")] Pharmacy pharmacy)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    // db.Pharmacy.Add(pharmacy);
                    db.addPharmacy(pharmacy.pharmacyAddress);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }

            return View(pharmacy);
        }

        // GET: Pharmacy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacy pharmacy = db.Pharmacy.Find(id);
            if (pharmacy == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy);
        }

        // POST: Pharmacy/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pharmacyNumber,pharmacyAddress")] Pharmacy pharmacy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Entry(pharmacy).State = EntityState.Modified;
                    db.updatePharmacy(pharmacy.pharmacyNumber, pharmacy.pharmacyAddress);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }
            return View(pharmacy);
        }

        // GET: Pharmacy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacy pharmacy = db.Pharmacy.Find(id);
            if (pharmacy == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy);
        }

        // POST: Pharmacy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Pharmacy pharmacy = db.Pharmacy.Find(id);
                //db.Pharmacy.Remove(pharmacy);
                db.deletePharmacy(pharmacy.pharmacyNumber);
                db.SaveChanges();
            }catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }
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
