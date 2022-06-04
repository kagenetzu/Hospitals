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
    public class SecondController : Controller
    {
        private MedicineEntities db = new MedicineEntities();
        public ActionResult Index()
        {

            ViewBag.hospitalNumber = new SelectList(db.Hospital, "hospitalNumber", "hospitalNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string hospitalNumber)
        {
            return Redirect("Second/Result?idHospital=" + hospitalNumber);
        }

        public ActionResult Result(int idHospital)
        {
            SqlParameter parameter = new SqlParameter("@idHospital", idHospital);
            var secondTask = db.Database.SqlQuery<secondTask_Result>("secondTask @idHospital", parameter);
            return View(secondTask);
        }
    }
}
