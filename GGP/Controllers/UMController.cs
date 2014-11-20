using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class UMController : Controller
    {
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.UnifOfMeasurements.ToList());
            }
        }

        public ActionResult Create()
        {
            return View(new UnifOfMeasurement());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UnifOfMeasurement um)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                if (db.UnifOfMeasurements.Any(x => x.Name == um.Name.Trim()))
                {
                    ModelState.AddModelError("", "ชื่อหน่วยซ้ำ");
                    return View(um);
                }

                db.UnifOfMeasurements.Add(um);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }

            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.UnifOfMeasurements.Find(id));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UnifOfMeasurement um)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                UnifOfMeasurement dbUM = db.UnifOfMeasurements.Find(um.Id);
                if (dbUM == null)
                {
                    return RedirectToAction("Index");
                }

                if (db.UnifOfMeasurements.Any(x=> x.Id != um.Id && x.Name == um.Name))
                {
                    ModelState.AddModelError("", "ชื่อหน่วยซ้ำ");
                    return View(um);
                }

                dbUM.Name = um.Name;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}