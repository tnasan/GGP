using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GGP.Controllers
{
    [Authorize]
    public class CostingController : Controller
    {
        // GET: Costing
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Batik()
        {
            return View();
        }

        public ActionResult Fashion()
        {
            return View();
        }
    }
}