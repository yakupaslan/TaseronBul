using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaseronBul.Models
{
    public partial class Teklif
    {
        public int IlanNo { get; set; }
        public String FirmaAdi { get; set; }
        public float Fiyat { get; set; }
        public DateTime Tarih { get; set; }
    }
}