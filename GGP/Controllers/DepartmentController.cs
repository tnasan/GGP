using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class DepartmentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}