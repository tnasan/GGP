﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.Bills.Include("Company").Include("Customer").Include("BillStatus").Include("BillARPayments.ARPayment").ToList());
            }
        }

        public ActionResult Create()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                ViewBag.Companies = new SelectList(db.Companies.ToList(), "Id", "Code");
                ViewBag.Customers = new SelectList(db.Customers.OrderBy(x => x.Name).ToList(), "Id", "Name");
                ViewBag.BillStatuses = db.BillStatus.ToList();

                return View(new Bill());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bill bill)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                db.Bills.Add(bill);
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
                ViewBag.Companies = new SelectList(db.Companies.ToList(), "Id", "Code");
                ViewBag.Customers = new SelectList(db.Customers.OrderBy(x => x.Name).ToList(), "Id", "Name");
                ViewBag.BillStatuses = db.BillStatus.ToList();

                Bill bill = db.Bills.Find(id);
                if (bill == null)
                {
                    return RedirectToAction("Index");
                }

                return View(bill);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bill bill)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                Bill dbBill = db.Bills.Find(bill.Id);
                if (dbBill == null)
                {
                    return RedirectToAction("Index");
                }

                dbBill.Number = bill.Number;
                dbBill.Amount = bill.Amount;
                dbBill.CustomerId = bill.CustomerId;
                dbBill.CompanyId = bill.CompanyId;
                dbBill.BillDate = bill.BillDate;
                dbBill.BillStatusId = bill.BillStatusId;
                dbBill.Remark = bill.Remark;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult GetRemainings(long companyId, long customerId)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                var bills = (from bill in db.Bills.Include("BillARPayments").ToList()
                             where bill.CompanyId == companyId
                             && bill.CustomerId == customerId
                             && bill.BillStatus.Status != "รับเงินแล้ว"
                             && (!bill.BillARPayments.Any() || bill.Amount > bill.BillARPayments.Sum(x => x.Amount))
                             select bill).ToList();

                return Json(bills.Select(x => new
                    {
                        Id = x.Id,
                        Number = x.Number,
                        Amount = x.Amount,
                        Remaining = x.Amount - (x.BillARPayments.Any() ? x.BillARPayments.Sum(y => y.Amount) : 0m)
                    }));
            }
        }
    }
}