using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class ShippingDetails
    {
        public string UserName {  get; set; }
        [Required(ErrorMessage ="Lütfen bir adres başlığı girinç")]
        public string AdresBasligi {  get; set; }
        [Required(ErrorMessage = "Lütfen bir adres girin.")]
        public string Adres {  get; set; }
        [Required(ErrorMessage = "Lütfen bir şehir girin.")]
        public string Sehir {  get; set; }
        [Required(ErrorMessage = "Lütfen bir semt girin.")]
        public string Semt {  get; set; }
        [Required(ErrorMessage = "Lütfen bir mahalle girin.")]
        public string Mahalle { get; set; }
        public string PostaKodu { get; set; }

    }
}