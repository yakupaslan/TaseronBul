using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaseronBul.Models;

namespace TaseronBul.Controllers
{
    public class AdminHomeController : Controller
    {
        TaseronBulEntities1 db = new TaseronBulEntities1();

        public ActionResult Index()
        {
            var YeniFirma=(from i in db.Firmas
                            where i.Durum == false
                             select i
                               ).ToList();
            ViewBag.YeniUye = YeniFirma.Count;

            var YeniIlan = (from b in db.Ilans where b.Durum == false select b).ToList();
            ViewBag.Ilan = YeniIlan.Count;

            return View();
        }

        public ActionResult YeniFirma() {

            return View();
        
        }

    }
}
