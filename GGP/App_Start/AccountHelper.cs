using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using GGP.Models;
using Microsoft.Ajax.Utilities;

namespace GGP
{
    public static class AccountHelper
    {
        static Account _account;

        static AccountHelper()
        {
            RefreshCache();
        }

        public static bool IsAuthenticated
        {
            get
            {
                return _account != null;
            }
        }

        public static Account CurrentAccount
        {
            get
            {
                return _account;
            }
        }

        public static string CurrentUsername
        {
            get
            {
                return _account != null ? _account.Username : null;
            }
        }

        public static string CurrentAccountName
        {
            get
            {
                return _account != null ? _account.Employees.Any() ? String.Format("{0} {1}",_account.Employees.First().Firstname, _account.Employees.First().Lastname) : _account.Username : null;
            }
        }

        public static void RefreshCache()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                if (HttpContext.Current.User != null)
                {
                    _account = db.Accounts.Find(HttpContext.Current.User.Identity.Name);
                }
                else
                {
                    _account = null;
                }
            }
        }
    }
}