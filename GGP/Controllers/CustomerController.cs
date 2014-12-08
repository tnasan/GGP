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
            using (GGPDBEntities customerDB = new GGPDBEntities())
            {
                var customerList = customerDB.Customers.Include("CustomerContacts").ToList();
                return View(customerList);
            }
        }

        public ActionResult Create()
        {
            return View(new Customer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            using (GGPDBEntities customerDB = new GGPDBEntities())
            {
                if (customer.CustomerContacts.Any())
                {
                    customer.CustomerContacts = customer.CustomerContacts.Where(x => !(String.IsNullOrEmpty(x.Name) || String.IsNullOrEmpty(x.Name.Trim()))).ToList();
                    customer.CustomerContacts.Select(x =>
                    {
                        x.Name = x.Name.Trim();
                        x.TelephoneNumber = x.TelephoneNumber.Trim();
                        x.Email = x.Email.Trim();
                        return x;
                    });
                }
                customerDB.Customers.Add(customer);
                customerDB.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            using (GGPDBEntities customerDB = new GGPDBEntities())
            {
                Customer dbCustomer = customerDB.Customers.Include("CustomerContacts").Single(x => x.Id == id);
                if (dbCustomer == null)
                {
                    return RedirectToAction("Index");
                }

                return View(dbCustomer);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            using (GGPDBEntities customerDB = new GGPDBEntities())
            {
                customer.CustomerContacts = customer.CustomerContacts.Where(x => !(String.IsNullOrEmpty(x.Name) || String.IsNullOrEmpty(x.Name.Trim()))).ToList();
                customer.CustomerContacts.Select(x =>
                {
                    x.Name = x.Name.Trim();
                    x.TelephoneNumber = x.TelephoneNumber.Trim();
                    x.Email = x.Email.Trim();
                    return x;
                });

                Customer dbCustomer = customerDB.Customers.Find(customer.Id);
                if (dbCustomer == null)
                {
                    return RedirectToAction("Index");
                }

                dbCustomer.Name = customer.Name;
                dbCustomer.TelephoneNumber = customer.TelephoneNumber;
                dbCustomer.FaxNumber = customer.FaxNumber;
                dbCustomer.Email = customer.Email;
                dbCustomer.WebsiteUrl = customer.WebsiteUrl;
                dbCustomer.Address = customer.Address;
                dbCustomer.CustomerContacts.Clear();
                dbCustomer.CustomerContacts = customer.CustomerContacts;
                customerDB.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult GetCustomers(string query)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                var customers = from customer in db.Customers
                                where customer.Name.Contains(query)
                                select customer;
                return Json(customers.Select(x =>
                    new
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList());
            }
        }
    }
}