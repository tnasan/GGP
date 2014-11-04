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
            return View(new Customer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            using (CustomerEntities customerDB = new CustomerEntities())
            {
                if (customer.Contacts.Any())
                {
                    customer.Contacts = customer.Contacts.Where(x => !(String.IsNullOrEmpty(x.Name) || String.IsNullOrEmpty(x.Name.Trim()))).ToList();
                    customer.Contacts.Select(x =>
                    {
                        x.Name = x.Name.Trim();
                        x.TelephoneNumber = x.TelephoneNumber.Trim();
                        x.Email = x.Email.Trim();
                        return x;
                    });
                    var existingContacts = (from contact in customer.Contacts
                                            join dbContact in customerDB.Contacts on new { contact.Name, contact.TelephoneNumber, contact.Email } equals new { dbContact.Name, dbContact.TelephoneNumber, dbContact.Email }
                                            select dbContact).ToList();
                    var newContacts = from contact in customer.Contacts
                                      where !existingContacts.Any(x => x.Name == contact.Name && x.TelephoneNumber == contact.TelephoneNumber && x.Email == contact.Email)
                                      select contact;
                    customer.Contacts = existingContacts.Union(newContacts).ToList();
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

            using (CustomerEntities customerDB = new CustomerEntities())
            {
                Customer dbCustomer = customerDB.Customers.Include("Contacts").Single(x => x.Id == id);
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
            using (CustomerEntities customerDB = new CustomerEntities())
            {
                customer.Contacts = customer.Contacts.Where(x => !(String.IsNullOrEmpty(x.Name) || String.IsNullOrEmpty(x.Name.Trim()))).ToList();
                customer.Contacts.Select(x =>
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

                var existingContacts = (from contact in customer.Contacts
                                        join dbContact in customerDB.Contacts on new { contact.Name, contact.TelephoneNumber, contact.Email } equals new { dbContact.Name, dbContact.TelephoneNumber, dbContact.Email }
                                        select dbContact).ToList();
                var newContacts = from contact in customer.Contacts
                                  where !existingContacts.Any(x => x.Name == contact.Name && x.TelephoneNumber == contact.TelephoneNumber && x.Email == contact.Email)
                                  select contact;

                dbCustomer.Contacts = existingContacts.Union(newContacts).ToList();
                customerDB.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}