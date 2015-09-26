using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GGP.Controllers
{
    public class CostingController : Controller
    {
        // GET: Costing
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cost()
        {
            return View();
        }
        
        public ActionResult Project()
        {
            return View();
        }
    }
}