using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class manualController : Controller
    {
        // GET: manual
        public ActionResult Index()
        {
            return View();
        }

      public ActionResult uv()
      {
         return View();
      }
    }
}