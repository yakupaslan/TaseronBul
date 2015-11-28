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
            return View();
        }

        [HttpPost]
        public ActionResult Create(Firma firma,int GirilenDeger,int sonuc)
        {
            if (ModelState.IsValid && GirilenDeger==sonuc)
            {
                db.Firmas.Add(firma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(firma);
        }

        [HttpPost]
        public ActionResult Create2(Uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Uyes.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uye);
        }
    }
}
