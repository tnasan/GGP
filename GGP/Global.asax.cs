using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using GGP.Models;
using System.Security.Principal;

namespace GGP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string m_cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie m_cookie = Context.Request.Cookies[m_cookieName];

            if (m_cookie != null)
            {
                try
                {
                    FormsAuthenticationTicket m_ticket = FormsAuthentication.Decrypt(m_cookie.Value);

                    GGPDBEntities db = new GGPDBEntities();
                    string m_roleList = String.Empty; //String.Join(",", db.Accounts.SingleOrDefault(x => x.Username == m_ticket.Name).Roles.Select(x => x.Name));

                    GenericIdentity m_identity = new GenericIdentity(m_ticket.Name);
                    GenericPrincipal m_principal = new GenericPrincipal(m_identity, new string[] { m_roleList });

                    Context.User = m_principal;
                    System.Threading.Thread.CurrentPrincipal = m_principal;
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
