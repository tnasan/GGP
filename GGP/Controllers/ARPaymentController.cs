using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class ARPaymentController : Controller
    {
        // GET: ARPayment
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.ARPayments.Include("PaymentMethod").Include("ARCheque").ToList());
            }
        }

        public ActionResult Create()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                ViewBag.PaymentMethods = db.PaymentMethods.ToList();
                ViewBag.Companies = new SelectList(db.Companies.ToList(), "Id", "Code");
                ViewBag.Banks = new SelectList(db.Banks.ToList(), "Id", "Abbreviation");
                ViewBag.ChequeStatuses = new SelectList(db.ChequeStatus.ToList(), "Id", "Name");

                return View(new ARPayment());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ARPayment payment)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                PaymentMethod chequeMethod = db.PaymentMethods.Single(x => x.Name == "เช็ค");
                if (payment.PaymentMethodId != chequeMethod.Id)
                {
                    payment.ARCheque = null;
                }

                db.ARPayments.Add(payment);
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
                ARPayment payment = db.ARPayments.Include("ARCheque").Single(x => x.Id == id);
                if (payment == null)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.PaymentMethods = db.PaymentMethods.ToList();
                ViewBag.Companies = new SelectList(db.Companies.ToList(), "Id", "Code");
                ViewBag.Banks = new SelectList(db.Banks.ToList(), "Id", "Abbreviation");
                ViewBag.ChequeStatuses = new SelectList(db.ChequeStatus.ToList(), "Id", "Name");

                return View(payment);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ARPayment payment)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                PaymentMethod chequeMethod = db.PaymentMethods.Single(x => x.Name == "เช็ค");
                ARPayment dbPayment = db.ARPayments.Find(payment.Id);
                dbPayment.CompanyId = payment.CompanyId;
                dbPayment.PaymentMethodId = payment.PaymentMethodId;
                dbPayment.Amount = payment.Amount;
                dbPayment.PaymentDate = payment.PaymentDate;
                if (chequeMethod.Id == payment.PaymentMethodId)
                {
                    if (dbPayment.ARCheque == null)
                    {
                        dbPayment.ARCheque = new ARCheque();
                    }
                    dbPayment.ARCheque.ChequeNumber = payment.ARCheque.ChequeNumber;
                    dbPayment.ARCheque.BankId = payment.ARCheque.BankId;
                    dbPayment.ARCheque.ChequeStatusId = payment.ARCheque.ChequeStatusId;
                    dbPayment.ARCheque.ChequeDate = payment.ARCheque.ChequeDate;
                }
                else
                {
                    db.ARCheques.Remove(db.ARCheques.Find(payment.Id));
                    dbPayment.ARCheque = null;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}