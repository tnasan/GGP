using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using GGP.Models;

namespace GGP.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.Accounts.Include("Employees").ToList());
            }
        }

        public ActionResult Create()
        {
            return View(new Account());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
            if (String.IsNullOrEmpty(account.Username) || String.IsNullOrEmpty(account.Username.Trim()))
            {
                return View(account);
            }

            if (String.IsNullOrEmpty(account.Password) || String.IsNullOrEmpty(account.Password.Trim()))
            {
                return View(account);
            }

            // Get Password & Salt
            Tuple<string, string> passwordAndSalt = EncryptionHelper.GetHashPassword(account.Password);

            using (GGPDBEntities db = new GGPDBEntities())
            {
                account.Password = passwordAndSalt.Item1;
                account.Salt = passwordAndSalt.Item2;
                db.Accounts.Add(account);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            using (GGPDBEntities db = new GGPDBEntities())
            {
                Account account = db.Accounts.Find(id);
                if (account == null)
                {
                    return RedirectToAction("Inxex");
                }

                account.Password = String.Empty;
                account.Salt = String.Empty;

                return View(account);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Account account)
        {
            if (String.IsNullOrEmpty(account.Password) || String.IsNullOrEmpty(account.Password.Trim()))
            {
                return View(account);
            }

            Tuple<string, string> passwordAndSalt = EncryptionHelper.GetHashPassword(account.Password);

            using (GGPDBEntities db = new GGPDBEntities())
            {
                Account dbAccount = db.Accounts.Find(account.Username);

                dbAccount.Password = passwordAndSalt.Item1;
                dbAccount.Salt = passwordAndSalt.Item2;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View(AccountHelper.CurrentAccount);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(Account account)
        {
            if (String.IsNullOrEmpty(account.Password) || String.IsNullOrEmpty(account.Password.Trim()))
            {
                return View(account);
            }

            Tuple<string, string> passwordAndSalt = EncryptionHelper.GetHashPassword(account.Password);

            using (GGPDBEntities db = new GGPDBEntities())
            {
                Account dbAccount = db.Accounts.Find(AccountHelper.CurrentAccount.Username);
                
                dbAccount.Password = passwordAndSalt.Item1;
                dbAccount.Salt = passwordAndSalt.Item2;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public JsonResult CheckUsernameExist(string userName)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                Account account = db.Accounts.Find(userName);

                return Json(account == null);
            }
        }
    }
}