using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaseronBul.Models;
using System.Web.Mvc;

namespace TaseronBul.Controllers
{
    public class IlanController : Controller
    {
        //
        // GET: /Ilan/
        TaseronBulEntities db = new TaseronBulEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create() {

            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            return View();
            ViewBag.Sehir = new SelectList(db.Sehirs, "Plaka", "Sehir");
        }

        [HttpPost]
        public ActionResult Create(Ilan ilan)
        {
            if (ModelState.IsValid)
            {

                db.Ilans.Add(ilan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ilan);
        }

        

    }
}
