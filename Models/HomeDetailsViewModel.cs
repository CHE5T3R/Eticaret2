using Eticaret.Entities;
using Eticaret2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class HomeDetailsViewModel
    {
        public Product Product { get; set; }
        public List<Comment> Comments { get; set; }
    }
}