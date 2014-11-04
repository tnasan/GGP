using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            using (CustomerEntities customerDB = new CustomerEntities())
            {
                var customerList = customerDB.Customers.ToList();
                return View(customerList);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            using (CustomerEntities customerDB = new CustomerEntities())
            {
                customerDB.Customers.Add(customer);
                customerDB.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}