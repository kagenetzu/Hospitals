using KURSLAB3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KURSLAB3.Controllers
{
    public class FirstController : Controller
    {
        private MedicineEntities db = new MedicineEntities();
        public ActionResult Index()
        {

            ViewBag.pharmacyNumber = new SelectList(db.Pharmacy, "pharmacyNumber", "pharmacyNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string pharmacyNumber)
        {
            return Redirect("First/Result?idPharmacy=" + pharmacyNumber);
        }

        public ActionResult Result(int idPharmacy)
        {
            SqlParameter parameter = new SqlParameter("@idPharmacy", idPharmacy);
            var firstTask = db.Database.SqlQuery<firstTask_Result>("firstTask @idPharmacy", parameter);
            return View(firstTask);
        }
    }
}
