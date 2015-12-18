using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaseronBul.Models;

namespace TaseronBul.Controllers
{
    public class ListeIlanController : Controller
    {

        TaseronBulEntities1 db = new TaseronBulEntities1();

        public ActionResult Index()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");

            ViewBag.sehir = new SelectList(db.Sehirs, "Plaka", "Sehir1");

            return View(db.Ilans.ToList());
        }


        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            ViewBag.sehir = new SelectList(db.Sehirs, "Plaka", "Sehir1");
            int deger = Convert.ToInt32(form["KategoriId"]);
            bool ihaleturu = Convert.ToBoolean(form["IhaleTuru"]);
            int fiyat = Convert.ToInt32(form["Fiyat"]);

            decimal minfiyat = 0;
            decimal maxfiyat = 0;

            if (fiyat == 1)
            {
                minfiyat = 0;
                maxfiyat = 1000;
            }

            if (fiyat == 2)
            {
                minfiyat = 1000;
                maxfiyat = 2500;
            }
            if (fiyat == 3)
            {
                minfiyat = 2500;
                maxfiyat = 10000;
            }

            if (fiyat == 4)
            {
                minfiyat = 10000;
                maxfiyat = 10000000;
            }

            byte sehir = Convert.ToByte(form["sehir"]);


            var liste = (from i in db.Ilans
                         join b in db.Uyes on i.UyeId equals b.UyeId    
                                 where  (b.Sehir == sehir) &&
                                (i.KategoriId == deger || deger == 0) &&
                                (i.IlanTuru == ihaleturu || ihaleturu != false || ihaleturu != true) &&
                                ((i.MaxFiyat >= minfiyat && i.MaxFiyat <= maxfiyat) || fiyat == 0)
                                 select i).ToList<Ilan>();


            return View(liste);
        }

    }
}
