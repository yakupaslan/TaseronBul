using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaseronBul.Controllers
{
    public class TeklifController : Controller
    {
        //
        // GET: /Teklif/

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Deneme(int GelenId) {

            ViewBag.deger = GelenId;
            return View();
        }

    }
}
