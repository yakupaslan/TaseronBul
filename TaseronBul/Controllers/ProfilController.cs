using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaseronBul.Models;
namespace TaseronBul.Controllers
{
    public class ProfilController : Controller
    {
        //
        // GET: /Profil/
        TaseronBulEntities1 db = new TaseronBulEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KabulIlan()
        {
            string a = HttpContext.User.Identity.Name;
            var uye = (from z in db.Uyes where z.Mail == a select z).FirstOrDefault();
            if (uye == null)
            {
                return HttpNotFound();
            }
            //var firma = (from z in db.Firmas  select z).ToList();
            //var kabulilan = (from teklif in db.FirmaTeklifs group teklif by new { teklif.IlanNo.Value} into g  join fir in db.Firmas on g.FirstOrDefault().FirmaId equals fir.FirmaId join ilan in db.Ilans on g.FirstOrDefault().IlanNo equals ilan.IlanNo where ilan.IlanKapanisTarihi < DateTime.Now  && uye.UyeId == ilan.UyeId orderby g.FirstOrDefault().IlanNo ascending select new KabulI { Fiyat = (double)g.OrderBy(z => z.Fiyat).FirstOrDefault().Fiyat, IlanNo = (int)g.FirstOrDefault().IlanNo, firma = fir.FirmaAdi, puan = (int)fir.Puan, Telefon = fir.Telefon, yetkili = fir.YetkiliAd + " " + fir.YetkiliSoyad,FirmaNo=fir.FirmaId }).ToList<KabulI>();
            return View();
        }
        public ActionResult AlinanIs()
        {
            string a = HttpContext.User.Identity.Name;
            var firma = (from z in db.Firmas where z.Mail == a select z).FirstOrDefault();
            if (firma == null)
            {
                return HttpNotFound();
            }
            var Alinanis = (from kabul in db.KabulEdilens join teklif in db.FirmaTeklifs on kabul.TeklifId equals teklif.Id join ilan in db.Ilans on teklif.IlanNo equals ilan.IlanNo join uye in db.Uyes on ilan.UyeId equals uye.UyeId where firma.FirmaId==teklif.FirmaId orderby ilan.SonIsGunu descending select new AlinanIs {Aciklama=ilan.Aciklama,Uye=uye.Ad+" "+uye.Soyad,SonTarih=(DateTime)ilan.SonIsGunu,IlanNo=ilan.IlanNo,Telefon=uye.Telefon,Fiyat=(float)teklif.Fiyat}).ToList<AlinanIs>(); 
            return View(Alinanis);
        }
        public ActionResult GuncelTeklif()
        {
            string a = HttpContext.User.Identity.Name;
            var firma = (from z in db.Firmas where z.Mail == a select z).FirstOrDefault();
            if (firma == null)
            {
                return HttpNotFound();
            }

            var ilan = (from teklif in db.FirmaTeklifs group teklif by new{ teklif.IlanNo.Value,teklif.FirmaId} into g  join fir in db.Firmas on g.FirstOrDefault().FirmaId equals fir.FirmaId join ilans in db.Ilans on g.FirstOrDefault().IlanNo equals ilans.IlanNo where g.FirstOrDefault().FirmaId == firma.FirmaId && ilans.IlanKapanisTarihi > DateTime.Now && ilans.IlanNo == g.FirstOrDefault().IlanNo orderby g.FirstOrDefault().IlanNo ascending select new GuncelT { Aciklama = ilans.Aciklama, Fiyat =(double) g.OrderBy(z=>z.Fiyat).FirstOrDefault().Fiyat, IlanNo = (int)g.FirstOrDefault().IlanNo, Tarih = (DateTime)ilans.IlanKapanisTarihi, tur = (Boolean)ilans.IlanTuru }).ToList<GuncelT>();
            return View(ilan);
        }
        public ActionResult EskiIlan()
        {
            string a = HttpContext.User.Identity.Name;
            var uye = (from z in db.Uyes where z.Mail == a select z).FirstOrDefault();
            if (uye == null)
            {
                return HttpNotFound();
            }
            var ilan = (from z in db.Ilans where z.IlanKapanisTarihi < DateTime.Now && z.UyeId == uye.UyeId select z).ToList();
            return View(ilan);
        }
        public ActionResult GuncelIlan()
        {
            string a = HttpContext.User.Identity.Name;
            var uye = (from z in db.Uyes where z.Mail == a select z).FirstOrDefault();
            if (uye == null)
            {
                return HttpNotFound();
            }
            var ilan = (from z in db.Ilans where z.IlanKapanisTarihi > DateTime.Now && z.UyeId==uye.UyeId select z).ToList();
            return View(ilan);
        }
        public ActionResult kontrol()
        {
            string a = HttpContext.User.Identity.Name;
            var uye = (from z in db.Uyes where z.Mail == a select z).FirstOrDefault();
            if (uye == null)
            {
                return RedirectToAction("Firma", "Profil");
            }
            else
            {
                return RedirectToAction("Uye", "Profil");
            }
        }
        [Authorize]
        public ActionResult Uye()
        {
            string a = HttpContext.User.Identity.Name;
            var uye = (from z in db.Uyes where z.Mail == a select z).FirstOrDefault();
            if (uye == null)
            {
                return HttpNotFound();
            }
            var sehir=(from i in db.Sehirs where i.Plaka==uye.Sehir select i.Sehir1).FirstOrDefault();
            ViewBag.sehir = sehir;
                return View(uye);
            
        }
        [Authorize]
        public ActionResult Firma()
        {
            string a = HttpContext.User.Identity.Name;
            var firma = (from z in db.Firmas where z.Mail == a select z).FirstOrDefault();
            if (firma == null)
            {
                return HttpNotFound();
            }
            var sehir = (from i in db.Sehirs where i.Plaka == firma.Sehir select i.Sehir1).FirstOrDefault();
            ViewBag.sehir = sehir;
            return View(firma);

        }
    }
}
