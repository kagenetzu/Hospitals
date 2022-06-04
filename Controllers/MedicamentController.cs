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
    public class MedicamentController : Controller
    {
        private MedicineEntities db = new MedicineEntities();

        // GET: Medicament
        public ActionResult Index(int pg = 1)
        {
            //return View(db.Medicament.ToList());

            List<Medicament> medicaments = db.Medicament.ToList();
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;

            int rescCount = medicaments.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = medicaments.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: Medicament/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medicament = db.Medicament.Find(id);
            if (medicament == null)
            {
                return HttpNotFound();
            }
            return View(medicament);
        }

        // GET: Medicament/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicament/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMedicament,nameMedicament,firmManufacturer")] Medicament medicament)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Medicament.Add(medicament);
                    db.addMedicament(medicament.nameMedicament, medicament.firmManufacturer);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }

            return View(medicament);
        }

        // GET: Medicament/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medicament = db.Medicament.Find(id);
            if (medicament == null)
            {
                return HttpNotFound();
            }
            return View(medicament);
        }

        // POST: Medicament/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMedicament,nameMedicament,firmManufacturer")] Medicament medicament)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //db.Entry(medicament).State = EntityState.Modified;
                    db.updateMedicament(medicament.idMedicament, medicament.nameMedicament, medicament.firmManufacturer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }
            return View(medicament);
        }

        // GET: Medicament/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medicament = db.Medicament.Find(id);
            if (medicament == null)
            {
                return HttpNotFound();
            }
            return View(medicament);
        }

        // POST: Medicament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Medicament medicament = db.Medicament.Find(id);
                //db.Medicament.Remove(medicament);
                db.deleteMedicament(medicament.idMedicament);
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
