using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.Companies.ToList());
            }
        }

        public ActionResult Create()
        {
            return View(new Company());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                db.Companies.Add(company);
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
                Company company = db.Companies.Find(id);
                if (company == null)
                {
                    return RedirectToAction("Index");
                }

                return View(company);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (company == null)
            {
                return RedirectToAction("Index");
            }

            using (GGPDBEntities db = new GGPDBEntities())
            {
                Company dbCompany = db.Companies.Find(company.Id);
                if (dbCompany == null)
                {
                    return RedirectToAction("Index");
                }

                dbCompany.Name = company.Name;
                dbCompany.TelephoneNumber = company.TelephoneNumber;
                dbCompany.FaxNumber = company.FaxNumber;
                dbCompany.WebsiteUrl = company.WebsiteUrl;
                dbCompany.Address = company.Address;
                dbCompany.Remark = company.Remark;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}