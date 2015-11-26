using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaseronBul.Models;

namespace TaseronBul.Controllers
{

    public class HomeController : Controller
    {
        TaseronBulEntities db = new TaseronBulEntities();
    
        public ActionResult Index()
        {
          
            return View(db.Ilans.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
