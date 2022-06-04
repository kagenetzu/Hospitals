using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using KURSLAB3.Models;

namespace KURSLAB3.Controllers
{
    public class HospitalController : Controller
    {
        private MedicineEntities db = new MedicineEntities();
        private Page a = new Page();

        // GET: Hospital
        public ActionResult Index(int pg=1)
        {

            List<Hospital> hospitals = db.Hospital.ToList();
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;

            int rescCount = hospitals.Count();
            var pages = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = hospitals.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: Hospital/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital hospital = db.Hospital.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            return View(hospital);
        }

        // GET: Hospital/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hospital/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hospitalNumber,hospitalAddress")] Hospital hospital)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.addHospital(hospital.hospitalAddress.ToString());
                    //db.Hospital.Add(hospital);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }catch(Exception ex)
            {

                
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }

            return View(hospital);
        }
       

        // GET: Hospital/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital hospital = db.Hospital.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            return View(hospital);
        }

        // POST: Hospital/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hospitalNumber,hospitalAddress")] Hospital hospital)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.updateHospital(hospital.hospitalNumber, hospital.hospitalAddress);
                    //db.Entry(hospital).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.InnerException.Message;
                return View("Error");
            }
            return View(hospital);
        }

        // GET: Hospital/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital hospital = db.Hospital.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            return View(hospital);
        }

        // POST: Hospital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Hospital hospital = db.Hospital.Find(id);
                db.deleteHospital(hospital.hospitalNumber);
                //db.Hospital.Remove(hospital);
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
