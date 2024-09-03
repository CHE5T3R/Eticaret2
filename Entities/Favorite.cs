using Eticaret.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}