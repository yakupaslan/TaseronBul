using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaseronBul.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;
namespace TaseronBul.Controllers
{
    public class IlanController : Controller
    {
        //
        // GET: /Ilan/
        TaseronBulEntities1 db = new TaseronBulEntities1();

        public ActionResult Index()
        {
            return View(db.Ilans.ToList());
        }
        public ActionResult IlanAyrintiA()
        {
            return View();
        }
        public ActionResult IlanKontT(int id)
        {
            var ilantür = (from z in db.Ilans where z.IlanNo == id select z.IlanTuru).FirstOrDefault();
            if (ilantür == false)
            {
                return RedirectToAction("IlanAyrintiA", "Ilan", new { id });
            }
            else
            {
                return RedirectToAction("IlanAyrintiK", "Ilan", new { id });
            }

        }
        [HttpPost]
        [Authorize]
        public ActionResult IlanAyrintiA(int id, int fiyat)
        {
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();

            }
            string a = HttpContext.User.Identity.Name;
            var tip = (from z in db.Firmas where z.Mail == a select z).FirstOrDefault();
            if (tip == null)
            {
                ModelState.AddModelError(string.Empty, "Teklif Vermeye yetkiniz yok");
            }
            else
            {
                var sontarih = (from z in db.Ilans where z.IlanNo == id select z.IlanKapanisTarihi).FirstOrDefault();
                if (sontarih > DateTime.Now)
                {
                    var fiyatkontrol = (from z in db.FirmaTeklifs where z.IlanNo == id select z.Fiyat).Min();
                    if (fiyatkontrol == null)
                    {
                        fiyatkontrol = (int)(from z in db.Ilans where id == z.IlanNo select z.MaxFiyat).Max();
                    }
                    if (fiyatkontrol > fiyat)
                    {


                        FirmaTeklif teklifver = new FirmaTeklif();
                        teklifver.IlanNo = id;
                        teklifver.Tarih = DateTime.Now;
                        teklifver.FirmaId = tip.FirmaId;
                        teklifver.Fiyat = fiyat;
                        db.FirmaTeklifs.Add(teklifver);
                        db.SaveChanges();


                        MailMessage mail = new MailMessage();

                        mail.From = new MailAddress("aslanyakup20@gmail.com");
                        var mailaaa = (from mail1 in db.Firmas where mail1.FirmaAdi == tip.FirmaAdi select mail1).FirstOrDefault();
                        mail.To.Add(mailaaa.Mail);

                        mail.Subject = "Teklif var";

                        
                        mail.Body = " Fiyat "+fiyat.ToString()+"  Teklif veren   "+mailaaa.FirmaAdi;

                        SmtpClient sc = new SmtpClient();

                        sc.Port = 587;
                        sc.Host = "smtp.gmail.com";

                        sc.EnableSsl = true;

                        sc.Credentials = new NetworkCredential("aslanyakup20@gmail.com", "5a52237320");

                        sc.Send(mail);
                        
                        


                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "Fiyatı Lütfen Kontrol Ediniz");

                    }

                }
            }
            var kategori = (from u in db.Kategoris where u.KategoriId == ilan.KategoriId select u).FirstOrDefault();
            ViewBag.Kategori = kategori.KategoriAdi;
            ViewBag.Ilantürü = "Açık ihale";
            ViewBag.IlanY = ((ilan.IlanYayinlamaTarihi).ToString()).ToString();
            ViewBag.IlanK = (ilan.IlanKapanisTarihi).ToString();
            ViewBag.Sonis = (ilan.SonIsGunu).ToString();
            ViewBag.MaxF = (ilan.MaxFiyat).ToString();
            ViewBag.Aciklama = ilan.Aciklama;
            var liste = (from teklif in db.FirmaTeklifs join firma in db.Firmas on teklif.FirmaId equals firma.FirmaId where teklif.IlanNo == id orderby teklif.Fiyat ascending select new Teklif { FirmaAdi = firma.FirmaAdi, Fiyat = (float)teklif.Fiyat, Tarih = (DateTime)teklif.Tarih, IlanNo = (int)teklif.IlanNo }).ToList<Teklif>();
            return View(liste);
        }
        [HttpGet]
        public ActionResult IlanAyrintiA(int id)
        {
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();

            }
            var ilantür = (from z in db.Ilans where z.IlanNo == id select z.IlanTuru).FirstOrDefault();
            if (ilantür == false)
            {
                var kategori = (from u in db.Kategoris where u.KategoriId == ilan.KategoriId select u).FirstOrDefault();
                ViewBag.Kategori = kategori.KategoriAdi;
                ViewBag.Ilantürü = "Açık ihale";
                ViewBag.IlanY = ((ilan.IlanYayinlamaTarihi).ToString()).ToString();
                ViewBag.IlanK = (ilan.IlanKapanisTarihi).ToString();
                ViewBag.Sonis = (ilan.SonIsGunu).ToString();
                ViewBag.MaxF = (ilan.MaxFiyat).ToString();
                ViewBag.Aciklama = ilan.Aciklama;

                var liste = (from teklif in db.FirmaTeklifs join firma in db.Firmas on teklif.FirmaId equals firma.FirmaId where teklif.IlanNo == id orderby teklif.Fiyat ascending select new Teklif { FirmaAdi = firma.FirmaAdi, Fiyat = (float)teklif.Fiyat, Tarih = (DateTime)teklif.Tarih, IlanNo = (int)teklif.IlanNo }).ToList<Teklif>();
              
                return View(liste);
            }
            else
            {
                return RedirectToAction("IlanAyrintiK", "Ilan", new { id });
            }


        }
        [HttpPost]
        [Authorize]
        public ActionResult IlanAyrintiK(int id, int fiyat)
        {
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();

            }
            string a = HttpContext.User.Identity.Name;
            var tip = (from z in db.Firmas where z.Mail == a select z).FirstOrDefault();
            if (tip == null)
            {
                ModelState.AddModelError(string.Empty, "Teklif Vermeye Yetkiniz Yok");
            }
            else
            {
                var kontrol = (from z in db.FirmaTeklifs where z.IlanNo == id && z.FirmaId==tip.FirmaId select z).FirstOrDefault();
                if (kontrol == null)
                {
                    var sontarih = (from z in db.Ilans where z.IlanNo == id select z.IlanKapanisTarihi).FirstOrDefault();
                if (sontarih > DateTime.Now)
                {
                    
                    var  fiyatkontrol = (int)(from z in db.Ilans where id == z.IlanNo select z.MaxFiyat).Max();
                    if (fiyatkontrol > fiyat)
                    {
                        FirmaTeklif teklifver = new FirmaTeklif();
                        teklifver.IlanNo = id;
                        teklifver.Tarih = DateTime.Now;
                        teklifver.FirmaId = tip.FirmaId;
                        teklifver.Fiyat = fiyat;
                        db.FirmaTeklifs.Add(teklifver);
                        db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Fiyatı Lütfen Kontrol Ediniz");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tarih");
                }
                }
                else
                {

                    //kapalı ilan güncelleme
                    var sontarih = (from z in db.Ilans where z.IlanNo == id select z.IlanKapanisTarihi).FirstOrDefault();
                    if (sontarih > DateTime.Now)
                    {
                        
                          var  fiyatkontrol = (int)(from z in db.Ilans where id == z.IlanNo select z.MaxFiyat).Max();
                        
                        if (fiyatkontrol > fiyat)
                        {
                            var teklifver = db.FirmaTeklifs.Find(14);
                            teklifver.IlanNo = id;
                            teklifver.Tarih = DateTime.Now;
                            teklifver.FirmaId = tip.FirmaId;
                            teklifver.Fiyat = fiyat;
                            db.SaveChanges();
                            ModelState.AddModelError(string.Empty, "Önceden Verdiğiniz Teklif Güncellenmiştir.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Fiyatı Lütfen Kontrol Ediniz");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Tarih");
                    }
                }
            }
            var kategori = (from u in db.Kategoris where u.KategoriId == ilan.KategoriId select u).FirstOrDefault();
            ViewBag.Kategori = kategori.KategoriAdi;
            ViewBag.Ilantürü = "Kapalı İhale";
            ViewBag.IlanY = ((ilan.IlanYayinlamaTarihi).ToString()).ToString();
            ViewBag.IlanK = (ilan.IlanKapanisTarihi).ToString();
            ViewBag.Sonis = (ilan.SonIsGunu).ToString();
            ViewBag.MaxF = (ilan.MaxFiyat).ToString();
            ViewBag.Aciklama = ilan.Aciklama;
            var liste = (from teklif in db.FirmaTeklifs join firma in db.Firmas on teklif.FirmaId equals firma.FirmaId where teklif.IlanNo == id orderby teklif.Fiyat ascending select new Teklif { FirmaAdi = firma.FirmaAdi, Fiyat = (float)teklif.Fiyat, Tarih = (DateTime)teklif.Tarih, IlanNo = (int)teklif.IlanNo }).ToList<Teklif>();
            return View(liste);
        }
        [HttpGet]
        public ActionResult IlanAyrintiK(int id)
        {
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();

            }
            var ilantür = (from z in db.Ilans where z.IlanNo == id select z.IlanTuru).FirstOrDefault();
            if (ilantür == true)
            {
                var kategori = (from u in db.Kategoris where u.KategoriId == ilan.KategoriId select u).FirstOrDefault();
                ViewBag.Kategori = kategori.KategoriAdi;
                ViewBag.Ilantürü = "Kapalı ihale";
                ViewBag.IlanY = ((ilan.IlanYayinlamaTarihi).ToString()).ToString();
                ViewBag.IlanK = (ilan.IlanKapanisTarihi).ToString();
                ViewBag.Sonis = (ilan.SonIsGunu).ToString();
                ViewBag.MaxF = (ilan.MaxFiyat).ToString();
                ViewBag.Aciklama = ilan.Aciklama;

                var liste = (from teklif in db.FirmaTeklifs join firma in db.Firmas on teklif.FirmaId equals firma.FirmaId where teklif.IlanNo == id orderby teklif.Fiyat ascending select new Teklif { FirmaAdi = firma.FirmaAdi, Fiyat = (float)teklif.Fiyat, Tarih = (DateTime)teklif.Tarih, IlanNo = (int)teklif.IlanNo }).ToList<Teklif>();

                return View(liste);
            }
            else
            {
                return RedirectToAction("IlanAyrintiA", "Ilan", new { id });
            }


        }
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Açık İhale", Value = "0" });

            items.Add(new SelectListItem { Text = "Kapalı İhale", Value = "1" });

            ViewBag.Tip = items;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Ilan ilan, int KategoriId,int Tip)
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Açık İhale", Value = "0" });
            items.Add(new SelectListItem { Text = "Kapalı İhale", Value = "1" });
            ViewBag.Tip = items;
            if (ModelState.IsValid)
            {
                string a = HttpContext.User.Identity.Name;
                var uye = (from z in db.Uyes where z.Mail == a select z).FirstOrDefault();
                if (uye == null)
                {
                    ModelState.AddModelError(string.Empty, "Teklif Vermeye yetkiniz yok");
                }
                else
                {
                ilan.UyeId = uye.UyeId;
                ilan.IlanTuru = Convert.ToBoolean(Tip);
                ilan.KategoriId =(byte)KategoriId;

                db.Ilans.Add(ilan);
                db.SaveChanges();
                return RedirectToAction("Index","Home");

                }
                
            }

            return View(ilan);
        }



    }
}
