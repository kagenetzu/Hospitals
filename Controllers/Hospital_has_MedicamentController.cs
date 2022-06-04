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
    public class Hospital_has_MedicamentController : Controller
    {
        private MedicineEntities db = new MedicineEntities();

        // GET: Hospital_has_Medicament
        public ActionResult Index(int pg = 1)
        {
            List<Hospital_has_Medicament> hhm = db.Hospital_has_Medicament.ToList();
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;

            int rescCount = hhm.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = hhm.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: Hospital_has_Medicament/Details/5
        public ActionResult Details(int? id1, int? id2)
        {
            
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital_has_Medicament hospital_has_Medicament = db.Hospital_has_Medicament.Find(id1,id2);
            if (hospital_has_Medicament == null)
            {
                return HttpNotFound();
            }
            return View(hospital_has_Medicament);
        }

        // GET: Hospital_has_Medicament/Create
        public ActionResult Create()
        {
            ViewBag.Hospital_hospitalNumber = new SelectList(db.Hospital, "hospitalNumber", "hospitalAddress");
            ViewBag.Medicament_idMedicament = new SelectList(db.Medicament, "idMedicament", "nameMedicament");
            return View();
        }

        // POST: Hospital_has_Medicament/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "Hospital_hospitalNumber,Medicament_idMedicament,countPackagesMedicament")] Hospital_has_Medicament hospital_has_Medicament)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    //db.Hospital_has_Medicament.Add(hospital_has_Medicament);
                    db.addHospitalHasMedicament(hospital_has_Medicament.Hospital_hospitalNumber,
                        hospital_has_Medicament.Medicament_idMedicament,
                        hospital_has_Medicament.countPackagesMedicament);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
                
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }

            ViewBag.Hospital_hospitalNumber = new SelectList(db.Hospital, "hospitalNumber", "hospitalAddress", hospital_has_Medicament.Hospital_hospitalNumber);
            ViewBag.Medicament_idMedicament = new SelectList(db.Medicament, "idMedicament", "nameMedicament", hospital_has_Medicament.Medicament_idMedicament);
            return View(hospital_has_Medicament);
        }

        // GET: Hospital_has_Medicament/Edit/5
        public ActionResult Edit(int? id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital_has_Medicament hospital_has_Medicament = db.Hospital_has_Medicament.Find(id1, id2);
            if (hospital_has_Medicament == null)
            {
                return HttpNotFound();
            }
            ViewBag.Hospital_hospitalNumber = new SelectList(db.Hospital, "hospitalNumber", "hospitalAddress", hospital_has_Medicament.Hospital_hospitalNumber);
            ViewBag.Medicament_idMedicament = new SelectList(db.Medicament, "idMedicament", "nameMedicament", hospital_has_Medicament.Medicament_idMedicament);
            return View(hospital_has_Medicament);
        }

        // POST: Hospital_has_Medicament/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hospital_hospitalNumber,Medicament_idMedicament,countPackagesMedicament")] Hospital_has_Medicament hospital_has_Medicament)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    //db.Entry(hospital_has_Medicament).State = EntityState.Modified;
                    db.updateHospitalHasMedicament(hospital_has_Medicament.Hospital_hospitalNumber,
                        hospital_has_Medicament.Medicament_idMedicament,
                        hospital_has_Medicament.countPackagesMedicament);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }
            ViewBag.Hospital_hospitalNumber = new SelectList(db.Hospital, "hospitalNumber", "hospitalAddress", hospital_has_Medicament.Hospital_hospitalNumber);
            ViewBag.Medicament_idMedicament = new SelectList(db.Medicament, "idMedicament", "nameMedicament", hospital_has_Medicament.Medicament_idMedicament);
            return View(hospital_has_Medicament);
        }

        // GET: Hospital_has_Medicament/Delete/5
        public ActionResult Delete(int? id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital_has_Medicament hospital_has_Medicament = db.Hospital_has_Medicament.Find(id1, id2);
            if (hospital_has_Medicament == null)
            {
                return HttpNotFound();
            }
            return View(hospital_has_Medicament);
        }

        // POST: Hospital_has_Medicament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id1, int id2)
        {
            try
            {
                Hospital_has_Medicament hospital_has_Medicament = db.Hospital_has_Medicament.Find(id1, id2);
                //db.Hospital_has_Medicament.Remove(hospital_has_Medicament);
                db.deleteHospitalHasMedicament(hospital_has_Medicament.Hospital_hospitalNumber,
                    hospital_has_Medicament.Medicament_idMedicament);
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
