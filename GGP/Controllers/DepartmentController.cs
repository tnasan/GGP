using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class DepartmentController : Controller
    {
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.Departments.ToList());
            }
        }

        public ActionResult Create()
        {
            return View(new Department());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (department == null)
            {
                return View(department);
            }

            using (GGPDBEntities db = new GGPDBEntities())
            {
                db.Departments.Add(department);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            using (GGPDBEntities db = new GGPDBEntities())
            {
                Department dbDepartment = db.Departments.Find(id);

                return View(dbDepartment);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (department == null || department.Id <= 0)
            {
                return RedirectToAction("Index");
            }
            using (GGPDBEntities db = new GGPDBEntities())
            {
                Department dbDepartment = db.Departments.Find(department.Id);
                if (dbDepartment == null)
                {
                    return RedirectToAction("Index");
                }

                dbDepartment.Name = department.Name;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}