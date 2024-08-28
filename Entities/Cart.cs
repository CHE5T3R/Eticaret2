using Eticaret.Entities;
using Eticaret2.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int CartLine2Id { get; set; }
        public CartLine2 CartLine2 { get; set; }
        public double Total {  get; set; }


    }

    public class CartLine2
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}