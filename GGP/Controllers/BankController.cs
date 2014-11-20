using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class BankController : Controller
    {
        // GET: Bank
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.Banks.ToList());
            }
        }

        public ActionResult Create()
        {
            return View(new Bank());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bank bank)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                if (db.Banks.Any(x=> x.Name == bank.Name || x.Abbreviation == bank.Abbreviation))
                {
                    ModelState.AddModelError("", "ชื่อธนาคารหรือตัวย่อซ้ำ");
                    return View(bank);
                }

                db.Banks.Add(bank);
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
                Bank dbBank = db.Banks.Find(id);
                if (dbBank == null)
                {
                    return RedirectToAction("Index");
                }

                return View(dbBank);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bank bank)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                if (db.Banks.Any(x => x.Name == bank.Name || x.Abbreviation == bank.Abbreviation))
                {
                    ModelState.AddModelError("", "ชื่อธนาคารหรือตัวย่อซ้ำ");
                    return View(bank);
                }

                Bank dbBank = db.Banks.Find(bank.Id);
                if (dbBank == null)
                {
                    return RedirectToAction("Index");
                }

                dbBank.Name = bank.Name;
                dbBank.Abbreviation = bank.Abbreviation;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}