using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaseronBul.Models
{
    public partial class AlinanIs
    {
        public int IlanNo { get; set; }
        public double Fiyat { get; set; }
        public DateTime SonTarih { get; set; }
        public String Aciklama { get; set; }
        public String Telefon { get; set; }
        public String Uye { get; set; }
    }
}