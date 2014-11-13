using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class NationalityController : Controller
    {
        // GET: Nationality
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.Nationalities.ToList());    
            }
        }

        public ActionResult Create()
        {
            return View(new Nationality());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Nationality nationality)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                db.Nationalities.Add(nationality);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            using (GGPDBEntities db = new GGPDBEntities())
            {
                Nationality dbNationality = db.Nationalities.Find(id);
                if (dbNationality == null)
                {
                    return RedirectToAction("Index");
                }

                return View(dbNationality);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Nationality nationality)
        {
            if (nationality == null || nationality.Id <= 0)
            {
                return RedirectToAction("Index");
            }

            using (GGPDBEntities db = new GGPDBEntities())
            {
                Nationality dbNationality = db.Nationalities.Find(nationality.Id);
                if (dbNationality == null)
                {
                    return RedirectToAction("Index");
                }

                dbNationality.Name = nationality.Name;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}