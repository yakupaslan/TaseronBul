using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using TaseronBul.Models;

namespace TaseronBul.Controllers
{
    public class KayitController : Controller
    {
        //
        // GET: /Kayit/
        private TaseronBulEntities1 db = new TaseronBulEntities1();
        public ActionResult Index()
        {
            ViewBag.SehirId = new SelectList(db.Sehirs, "Plaka", "Sehir1");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Firma firma,byte Plaka)
        {
            ViewBag.SehirId = new SelectList(db.Sehirs, "Plaka", "Sehir1");
            if (ModelState.IsValid)
            {
                firma.Tip = 1;
                firma.Sehir = Plaka;
                db.Firmas.Add(firma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(firma);
        }

        [HttpPost]
        public ActionResult Create2(Uye uye, byte Plaka)
        {
            ViewBag.SehirId = new SelectList(db.Sehirs, "Plaka", "Sehir1");
            if (ModelState.IsValid)
            {
                uye.Tip = 2;
                uye.Sehir =Plaka;
                db.Uyes.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uye);
        }
    }
}
