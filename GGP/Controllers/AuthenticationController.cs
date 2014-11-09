using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;
using System.Web.Security;

namespace GGP.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Login()
        {
            return View(new Account());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account account)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                Account dbAccount = db.Accounts.Find(account.Username);
                if (dbAccount != null && dbAccount.Password == EncryptionHelper.GetHashPassword(account.Password, dbAccount.Salt))
                {
                    FormsAuthentication.SetAuthCookie(dbAccount.Username, true);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(account);
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}