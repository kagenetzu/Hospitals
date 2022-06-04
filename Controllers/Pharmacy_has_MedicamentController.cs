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
    public class Pharmacy_has_MedicamentController : Controller
    {
        private MedicineEntities db = new MedicineEntities();

        // GET: Pharmacy_has_Medicament
        public ActionResult Index(int pg =1)
        {
            //var pharmacy_has_Medicament = db.Pharmacy_has_Medicament.Include(p => p.Medicament).Include(p => p.Pharmacy);
            //return View(pharmacy_has_Medicament.ToList());
            List<Pharmacy_has_Medicament> phm = db.Pharmacy_has_Medicament.ToList();
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;

            int rescCount = phm.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = phm.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: Pharmacy_has_Medicament/Details/5
        public ActionResult Details(int? id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacy_has_Medicament pharmacy_has_Medicament = db.Pharmacy_has_Medicament.Find(id1,id2);
            if (pharmacy_has_Medicament == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy_has_Medicament);
        }

        // GET: Pharmacy_has_Medicament/Create
        public ActionResult Create()
        {
            ViewBag.Medicament_idMedicament = new SelectList(db.Medicament, "idMedicament", "nameMedicament");
            ViewBag.Pharmacy_pharmacyNumber = new SelectList(db.Pharmacy, "pharmacyNumber", "pharmacyAddress");
            return View();
        }

        // POST: Pharmacy_has_Medicament/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pharmacy_pharmacyNumber,Medicament_idMedicament,priceMedicamentPerPackeges,countPackegesMedicament")] Pharmacy_has_Medicament pharmacy_has_Medicament)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Pharmacy_has_Medicament.Add(pharmacy_has_Medicament);
                    db.addPharmacyHasMedicament(pharmacy_has_Medicament.Pharmacy_pharmacyNumber,
                        pharmacy_has_Medicament.Medicament_idMedicament,
                        pharmacy_has_Medicament.priceMedicamentPerPackeges,
                        pharmacy_has_Medicament.countPackegesMedicament);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }

            ViewBag.Medicament_idMedicament = new SelectList(db.Medicament, "idMedicament", "nameMedicament", pharmacy_has_Medicament.Medicament_idMedicament);
            ViewBag.Pharmacy_pharmacyNumber = new SelectList(db.Pharmacy, "pharmacyNumber", "pharmacyAddress", pharmacy_has_Medicament.Pharmacy_pharmacyNumber);
            return View(pharmacy_has_Medicament);
        }

        // GET: Pharmacy_has_Medicament/Edit/5
        public ActionResult Edit(int? id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacy_has_Medicament pharmacy_has_Medicament = db.Pharmacy_has_Medicament.Find(id1,id2);
            if (pharmacy_has_Medicament == null)
            {
                return HttpNotFound();
            }
            ViewBag.Medicament_idMedicament = new SelectList(db.Medicament, "idMedicament", "nameMedicament", pharmacy_has_Medicament.Medicament_idMedicament);
            ViewBag.Pharmacy_pharmacyNumber = new SelectList(db.Pharmacy, "pharmacyNumber", "pharmacyAddress", pharmacy_has_Medicament.Pharmacy_pharmacyNumber);
            return View(pharmacy_has_Medicament);
        }

        // POST: Pharmacy_has_Medicament/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pharmacy_pharmacyNumber,Medicament_idMedicament,priceMedicamentPerPackeges,countPackegesMedicament")] Pharmacy_has_Medicament pharmacy_has_Medicament)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // db.Entry(pharmacy_has_Medicament).State = EntityState.Modified;
                    db.updatePharmacyHasMedicament(pharmacy_has_Medicament.Pharmacy_pharmacyNumber,
                            pharmacy_has_Medicament.Medicament_idMedicament,
                            pharmacy_has_Medicament.priceMedicamentPerPackeges,
                            pharmacy_has_Medicament.countPackegesMedicament);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }
            ViewBag.Medicament_idMedicament = new SelectList(db.Medicament, "idMedicament", "nameMedicament", pharmacy_has_Medicament.Medicament_idMedicament);
            ViewBag.Pharmacy_pharmacyNumber = new SelectList(db.Pharmacy, "pharmacyNumber", "pharmacyAddress", pharmacy_has_Medicament.Pharmacy_pharmacyNumber);
            return View(pharmacy_has_Medicament);
        }

        // GET: Pharmacy_has_Medicament/Delete/5
        public ActionResult Delete(int? id1, int? id2)
        {
            if (id1 == null || id2 ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacy_has_Medicament pharmacy_has_Medicament = db.Pharmacy_has_Medicament.Find(id1, id2);
            if (pharmacy_has_Medicament == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy_has_Medicament);
        }

        // POST: Pharmacy_has_Medicament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id1, int id2)
        {
            try
            {
                Pharmacy_has_Medicament pharmacy_has_Medicament = db.Pharmacy_has_Medicament.Find(id1,id2);
                //db.Pharmacy_has_Medicament.Remove(pharmacy_has_Medicament);
                db.deletePharmacyHasMedicament(pharmacy_has_Medicament.Pharmacy_pharmacyNumber,
                    pharmacy_has_Medicament.Medicament_idMedicament);
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
