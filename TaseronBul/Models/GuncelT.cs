using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaseronBul.Models
{
    public partial class GuncelT
    {
        public int IlanNo { get; set; }
        public double Fiyat { get; set; }
        public DateTime Tarih { get; set; }
        public String Aciklama { get; set; }
        public Boolean tur{ get; set; }
    }
}