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
    public class FirmaAdminController : Controller
    {
        private TaseronBulEntities1 db = new TaseronBulEntities1();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            var firmas = db.Firmas.Include(f => f.Sehir1);
            return View(firmas.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            Firma firma = db.Firmas.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            ViewBag.Sehir = new SelectList(db.Sehirs, "Plaka", "Sehir1");
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Firma firma)
        {
            if (ModelState.IsValid)
            {
                db.Firmas.Add(firma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Sehir = new SelectList(db.Sehirs, "Plaka", "Sehir1", firma.Sehir);
            return View(firma);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Firma firma = db.Firmas.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sehir = new SelectList(db.Sehirs, "Plaka", "Sehir1", firma.Sehir);
            return View(firma);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Firma firma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sehir = new SelectList(db.Sehirs, "Plaka", "Sehir1", firma.Sehir);
            return View(firma);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Firma firma = db.Firmas.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            
            return View(firma);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Firma firma = db.Firmas.Find(id);
            db.Firmas.Remove(firma);
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