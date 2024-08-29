using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Entities
{
    public class Address
    { 
        public int Id { get; set; }
        public string UserName { get; set; }
        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
        public string PostaKodu { get; set; }
    }
}