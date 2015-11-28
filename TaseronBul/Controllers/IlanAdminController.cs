using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaseronBul.Models;

namespace TaseronBul.Controllers
{
    public class IlanAdminController : Controller
    {
        private TaseronBulEntities1 db = new TaseronBulEntities1();

        //
        // GET: /IlanAdmin/

        public ActionResult Index()
        {
            var ilans = db.Ilans.Include(i => i.Uye).Include(i => i.Kategori);

            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            return View(ilans.ToList());
        }


        [HttpPost]
        public ActionResult Index(FormCollection form) {


            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");


             int KategoriId = Convert.ToInt32(form["KategoriId"]);
             DateTime IlanYayinlamaTarihi = Convert.ToDateTime(form["IlkTarih"]);
             DateTime IlanKapanisTarihi = Convert.ToDateTime(form["SonTarih"]);
            
          var YeniListe = (from il in db.Ilans
                                    where il.IlanYayinlamaTarihi >=IlanYayinlamaTarihi  &&
                                    il.IlanKapanisTarihi>= IlanKapanisTarihi && 
                                    il.KategoriId==KategoriId
                                    select il
                               ).ToList();
            return View(YeniListe);
        }

        public ActionResult Details(int id = 0)
        {
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        //
        // GET: /IlanAdmin/Create

        public ActionResult Create()
        {
            ViewBag.UyeId = new SelectList(db.Uyes, "UyeId", "Ad");
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            return View();
        }

        //
        // POST: /IlanAdmin/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                db.Ilans.Add(ilan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UyeId = new SelectList(db.Uyes, "UyeId", "Ad", ilan.UyeId);
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi", ilan.KategoriId);
            return View(ilan);
        }

        //
        // GET: /IlanAdmin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            ViewBag.UyeId = new SelectList(db.Uyes, "UyeId", "Ad", ilan.UyeId);
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi", ilan.KategoriId);
            return View(ilan);
        }

        //
        // POST: /IlanAdmin/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ilan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UyeId = new SelectList(db.Uyes, "UyeId", "Ad", ilan.UyeId);
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi", ilan.KategoriId);
            return View(ilan);
        }

        //
        // GET: /IlanAdmin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        //
        // POST: /IlanAdmin/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ilan ilan = db.Ilans.Find(id);
            db.Ilans.Remove(ilan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}