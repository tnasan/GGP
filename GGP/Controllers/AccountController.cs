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

            // Generate Salt
            byte[] salt = GetSalt();
            // Generate Hash Password
            byte[] password = GetHashPassword(account.Password, salt);

            using (GGPDBEntities db = new GGPDBEntities())
            {
                account.Password = String.Join("", password.Select(x => x.ToString("X2")));
                account.Salt = String.Join("", salt.Select(x => x.ToString("X2")));
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

            using (GGPDBEntities db = new GGPDBEntities())
            {
                Account dbAccount = db.Accounts.Find(account.Username);

                byte[] salt = GetSalt();
                byte[] password = GetHashPassword(account.Password, salt);

                dbAccount.Password = String.Join("", password.Select(x => x.ToString("X2")));
                dbAccount.Salt = String.Join("", salt.Select(x => x.ToString("X2")));
                db.SaveChanges();

                return RedirectToAction("Index");
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

        private byte[] ConvertStringToByteArray(string str)
        {
            byte[] result = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, result, 0, result.Length);

            return result;
        }

        private byte[] GetSalt()
        {
            byte[] salt = new byte[64];

            RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(salt);

            return salt;
        }

        private byte[] GetHashPassword(string password, byte[] salt)
        {
            SHA512 sha512 = new SHA512Managed();
            byte[] hashPassword = sha512.ComputeHash(salt.Union(ConvertStringToByteArray(password)).ToArray());

            return hashPassword;
        }
    }
}