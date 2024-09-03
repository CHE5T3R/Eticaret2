using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eticaret.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [DisplayName("Ürün Adı")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Ürün Açıklaması")]
        [Required]
        public string Description { get; set; }
        [DisplayName("Ürün Fiyatı")]
        [Required]
        public double Price { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Image { get; set; }
        [DisplayName("Anasayfada mı")]
        [Required]
        public bool IsHome { get; set; }
        [DisplayName("Onaylı mı")]
        [Required]
        public bool IsApproved { get; set; }

        [Required]

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}