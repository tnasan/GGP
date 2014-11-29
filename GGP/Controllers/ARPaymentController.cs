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
                return View(db.ARPayments.Include("ARCheque").ToList());
            }
            return View();
        }

        public ActionResult Create()
        {
            return View(new ARPayment());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ARPayment payment)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
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
                ARPayment payment = db.ARPayments.Find(id);
                if (payment == null)
                {
                    return RedirectToAction("Index");
                }

                return View(payment);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ARPayment payment)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                PaymentMethod chequeMethod = db.PaymentMethods.Single(x=>x.Name == "เช้ค");
                ARPayment dbPayment = db.ARPayments.Find(payment.Id);
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
                    dbPayment.ARCheque = null;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}