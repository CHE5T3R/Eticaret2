using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string Img {  get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}