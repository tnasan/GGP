using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models.Supplier;

namespace GGP.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            using (SupplierEntities supplierDB = new SupplierEntities())
            {
                return View(supplierDB.Suppliers.Include("Contacts").ToList());
            }
        }

        public ActionResult Create()
        {
            return View(new Supplier());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            using (SupplierEntities supplierDB = new SupplierEntities())
            {
                if (supplier.Contacts.Any())
                {
                    supplier.Contacts = supplier.Contacts.Where(x => !(String.IsNullOrEmpty(x.Name) || String.IsNullOrEmpty(x.Name.Trim()))).ToList();
                    supplier.Contacts.Select(x =>
                    {
                        x.Name = x.Name.Trim();
                        x.TelephoneNumber = x.TelephoneNumber.Trim();
                        x.Email = x.Email.Trim();
                        return x;
                    });
                }
                supplierDB.Suppliers.Add(supplier);
                supplierDB.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            using (SupplierEntities supplierDB = new SupplierEntities())
            {
                Supplier supplier = supplierDB.Suppliers.Include("Contacts").SingleOrDefault(x => x.Id == id);

                if (supplier == null)
                {
                    return RedirectToAction("Index");
                }

                return View(supplier);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            using (SupplierEntities supplierDB = new SupplierEntities())
            {
                supplier.Contacts = supplier.Contacts.Where(x => !(String.IsNullOrEmpty(x.Name) || String.IsNullOrEmpty(x.Name.Trim()))).ToList();
                supplier.Contacts.Select(x =>
                {
                    x.Name = x.Name.Trim();
                    x.TelephoneNumber = x.TelephoneNumber.Trim();
                    x.Email = x.Email.Trim();
                    return x;
                });

                Supplier dbSupplier = supplierDB.Suppliers.Find(supplier.Id);
                if (dbSupplier == null)
                {
                    return RedirectToAction("Index");
                }

                dbSupplier.Name = supplier.Name;
                dbSupplier.TelephoneNumber = supplier.TelephoneNumber;
                dbSupplier.FaxNumber = supplier.FaxNumber;
                dbSupplier.Email = supplier.Email;
                dbSupplier.WebsiteUrl = supplier.WebsiteUrl;
                dbSupplier.Address = supplier.Address;
                dbSupplier.Contacts.Clear();
                dbSupplier.Contacts = supplier.Contacts;
                supplierDB.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}