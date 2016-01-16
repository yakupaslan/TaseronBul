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
        TaseronBulEntities1 db = new TaseronBulEntities1();
    
        public ActionResult Index()
        {
            
            
            var ilan = (from z in db.Ilans where z.IlanKapanisTarihi > DateTime.Now select z).ToList();
            return View(ilan);
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
